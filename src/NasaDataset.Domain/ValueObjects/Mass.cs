using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaDataset.Domain.ValueObjects
{
    public record Mass(decimal Value)
    {
        public static Mass FromString(string massString)
        => new(decimal.TryParse(massString, out var mass) ? mass : 0);
    }
}
