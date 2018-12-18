using System;

namespace ServiceApp.Core.Charge
{
    public interface IUnitConverter
    {
        long Convert(double amount, string currency);
    }

    public class UnitConverter : IUnitConverter
    {
        private readonly IRoundAmount _roundService;
        private readonly ICalculateUnits _unitCalculator;
        private readonly ICurrenciesStore _currenciesStore;

        public UnitConverter(
            IRoundAmount roundService,
            ICalculateUnits unitCalculator,
            ICurrenciesStore currenciesStore)
        {
            _roundService = roundService ?? throw new ArgumentNullException(nameof(roundService));
            _unitCalculator = unitCalculator ?? throw new ArgumentNullException(nameof(unitCalculator));
            _currenciesStore = currenciesStore ?? throw new ArgumentNullException(nameof(currenciesStore));
        }

        public long Convert(double amount, string currency)
        {
            var allSupportedCurrencies = _currenciesStore.Get();
            var amountOfUnits = _unitCalculator.CalculateUnits(amount, currency, allSupportedCurrencies);
            var roundedAmountOfUnits = _roundService.Round(amountOfUnits);

            return (long)roundedAmountOfUnits;
        }
    }
}