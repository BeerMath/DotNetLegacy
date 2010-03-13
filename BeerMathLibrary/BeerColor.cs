
using System;

namespace BeerMath
{
	// represents the types of beer color calculations
	public enum BeerColorType { Mcu, Srm, Ebc }

	// represents a beer's color
	public class BeerColor
	{
		private Decimal _Value;
		private BeerColorType _BCT;
		
		// some constants we'll use to convert between the colors
		private static double SrmFactor = 1.4922;
		private static double SrmExponent = 0.6859;
		private static decimal EcuFactor = 1.97m;
		
		public BeerColor ()
		{
			_Value = 0;
			_BCT = BeerColorType.Mcu;
		}
		public BeerColor (decimal Color, BeerColorType bct)
		{
			_Value = Color;
			_BCT = bct;
		}
		
		public decimal Srm
		{
			get { return _GetSrm(); }
		}
		public decimal Mcu
		{
			get { return _GetMcu(); }
		}
		public decimal Ebc
		{
			get { return _GetEbc(); }
		}
		
		// safe to swap this _to_ decimal (but not from, since we wouldn't know the units)
		public static implicit operator Decimal(BeerColor b)
		{
			return b._Value;
		}
		
		public override string ToString ()
		{
			return string.Format("{0} {1}", _Value, _BCT);
		}
		
		#region Conversions between color formulations
		private decimal _GetEbc()
		{
			if(BeerColorType.Ebc == _BCT)
				return _Value;
			if(BeerColorType.Srm == _BCT || BeerColorType.Mcu == _BCT)
				return EcuFactor * _GetSrm();
			
			throw new NotSupportedException();
		}
		
		private decimal _GetSrm()
		{
			if(BeerColorType.Ebc == _BCT)
				return _Value / EcuFactor;
			if(BeerColorType.Srm == _BCT)
				return _Value;
			if(BeerColorType.Mcu == _BCT)
				return (decimal)(SrmFactor * System.Math.Pow((double)_Value, SrmExponent));
			
			throw new NotSupportedException();
		}
		
		private decimal _GetMcu()
		{
			if(BeerColorType.Ebc == _BCT || BeerColorType.Srm == _BCT)
			{
				/* 
				 * more math than I've done since college!
				 * 
				 * Algebraic conversion on MCU->SRM conversion yields:
				 *   MCU = root((SRM/SrmFactor), SrmExponent)
				 * Applying logarithm rule yields:
				 *   log(MCU) = log(SRM/SrmFactor) / SrmExponent
				 * Finally, to get MCU back from log(MCU), raise the correct base to the other side:
				 *   MCU = base ^ (log(SRM/SrmFactor) / SrmExponent)
				*/
				return (decimal)Math.Pow(Math.E, (Math.Log((double)_GetSrm() / SrmFactor) / SrmExponent));
			}
			if(BeerColorType.Mcu == _BCT)
				return _Value;
			
			throw new NotSupportedException();
		}
		#endregion
	}
}
