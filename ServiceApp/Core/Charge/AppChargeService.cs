using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ServiceApp.Core.Charge.Models;

namespace ServiceApp.Core.Charge
{
    public class AppChargeService : IRoundAmount, ICalculateUnits, ICheckStatus
    {
        public double Round(double amount) => Math.Round(amount);

        public double CalculateUnits(double originalAmount, string normalizedCurrency, IEnumerable<Currency> currencies)
        {
            var currency = currencies.FirstOrDefault(x => x.NormalizedName == normalizedCurrency)
                           ?? throw new ArgumentException($"Unsupported currency '{normalizedCurrency}'.");

            return originalAmount * currency.UnitRatio;
        }

        public bool IsSuccessful(string status)
        {
            var normalizedStatus = status.ToUpper(CultureInfo.CurrentCulture);

            return normalizedStatus is ChargeConstant.Succeeded || 
                   normalizedStatus is ChargeConstant.Updated;
        }
    }
}