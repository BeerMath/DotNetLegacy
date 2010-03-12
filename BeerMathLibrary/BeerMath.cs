using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerMath
{
    public class Malt
    {
        public static decimal CalculateMcu(decimal GrainLbs, decimal DegreesLovibond, decimal VolumeGallons)
        {
            return (GrainLbs * DegreesLovibond) / VolumeGallons;
        }
    }
}
