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

	    public static decimal CalculateSrm(decimal GrainLbs, decimal DegreesLovibond, decimal VolumeGallons)
        {
            return 1.4922m * (decimal)System.Math.Pow((double)CalculateMcu(GrainLbs, DegreesLovibond, VolumeGallons), 0.6859);
        }
	}
}
