using System.Collections.Generic;
using ServiceApp.Core.Charge;
using ServiceApp.Core.Charge.Models;

namespace ServiceApp.Infrastructure.Stores
{
    public class CurrenciesStore : ICurrenciesStore
    {
        public IEnumerable<Currency> Get()
        {
            return new[]
            {
                new Currency { Name = "USA", UnitRatio = 100 }
            };
        }
    }
}