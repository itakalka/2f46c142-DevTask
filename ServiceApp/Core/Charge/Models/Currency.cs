using System.Globalization;

namespace ServiceApp.Core.Charge.Models
{
    public sealed class Currency
    {
        public string Name { get; set; }

        public string NormalizedName => Name.ToUpper(CultureInfo.CurrentCulture);

        public int UnitRatio { get; set; } = 1;
    }
}