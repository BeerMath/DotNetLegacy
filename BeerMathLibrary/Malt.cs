using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerMath
{
    public class Malt
    {
        public static BeerColor CalculateMcu(decimal GrainLbs, decimal DegreesLovibond, decimal VolumeGallons)
        {
            return new BeerColor((GrainLbs * DegreesLovibond) / VolumeGallons, BeerColorType.Mcu);
        }

	    public static BeerColor CalculateSrm(decimal GrainLbs, decimal DegreesLovibond, decimal VolumeGallons)
        {
			BeerColor Mcu = new BeerColor((GrainLbs * DegreesLovibond) / VolumeGallons, BeerColorType.Mcu);
			return new BeerColor(Mcu.Srm, BeerColorType.Srm);
        }
		
		public static BeerColor CalculateEbc(decimal GrainLbs, decimal DegreesLovibond, decimal VolumeGallons)
		{
			BeerColor Mcu = new BeerColor((GrainLbs * DegreesLovibond) / VolumeGallons, BeerColorType.Mcu);
			return new BeerColor(Mcu.Ebc, BeerColorType.Ebc);
		}
	}
}
