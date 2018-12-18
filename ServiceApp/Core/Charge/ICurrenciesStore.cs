using System.Collections.Generic;
using ServiceApp.Core.Charge.Models;

namespace ServiceApp.Core.Charge
{
    public interface ICurrenciesStore
    {
        IEnumerable<Currency> Get();
    }
}