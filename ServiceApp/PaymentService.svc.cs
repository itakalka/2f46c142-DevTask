using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;
using ServiceApp.Core.Charge;
using ServiceApp.Infrastructure.ExtraData;
using Stripe;

namespace ServiceApp
{
    public class PaymentService : IPaymentService
    {
        private readonly ChargeService _chargeService;
        private readonly IUnitConverter _unitConverter;
        private readonly ICheckStatus _statusChecker;

        public PaymentService(
            ChargeService chargeService,
            IUnitConverter unitConverter, 
            ICheckStatus statusChecker)
        {
            // We do not check arguments here to make fail free service.
            _chargeService = chargeService;
            _unitConverter = unitConverter;
            _statusChecker = statusChecker;
        }

        public bool Transact(string customerId, double amount, string currency, string cardId, Dictionary<string, string> extraData)
        {
            var eData = Mapper.Map<Dictionary<string, string>, ExtraData>(extraData);
            eData.IdempotencyKey = eData.IdempotencyKey ?? Guid.NewGuid().ToString();

            Trace.TraceInformation($"Start processing transaction with key: '{eData.IdempotencyKey}' " +
                                   $"for " +
                                   $"customer: {customerId}, " +
                                   $"amount: {amount}, " +
                                   $"currency: {currency}, " +
                                   $"card: {cardId}, " +
                                   $"extra data: {extraData}.");

            try
            {
                var amountToCharge = _unitConverter.Convert(amount, currency);

                Trace.TraceInformation($"Transaction with key: '{eData.IdempotencyKey}' converted amount: '{amount}' to units: '{amountToCharge}'.");

                // We bound to the contract and have to make a sync call.
                var charge = _chargeService.Create(
                    new ChargeCreateOptions
                    {
                        Amount = amountToCharge,
                        Currency = currency,
                        Description = eData.Description,
                        SourceId = cardId,
                        CustomerId = customerId
                    },
                    new RequestOptions
                    {
                        IdempotencyKey = eData.IdempotencyKey
                    });

                Trace.TraceInformation($"Transaction with key: '{eData.IdempotencyKey}' sent request to Stripe. " +
                                       $"Response: '{charge.StripeResponse.ResponseJson}'");

                var isSuccessful = _statusChecker.IsSuccessful(charge.Status);

                if (isSuccessful is true)
                {
                    Trace.TraceInformation($"Transaction with key: '{eData.IdempotencyKey}' was successful with status: '{charge.Status}'.");
                }
                else
                {
                    Trace.TraceWarning($"Transaction with key: '{eData.IdempotencyKey}' was not successful " +
                                       $"with " +
                                       $"status: '{charge.Status}', " +
                                       $"failure code: {charge.FailureCode}, " +
                                       $"failure message: {charge.FailureMessage}.");
                }

                return isSuccessful;
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Transaction with key: '{eData.IdempotencyKey}' was failed with " +
                                 $"exception: {ex}");
                return false;
            }
            
        }
    }
}
