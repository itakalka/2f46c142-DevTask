using System.Collections.Generic;
using ServiceApp.Core.Charge.Models;

namespace ServiceApp.Core.Charge
{
    public interface ICalculateUnits
    {
        double CalculateUnits(double originalAmount, string currency, IEnumerable<Currency> currencies);
    }
}