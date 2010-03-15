// 
//  Author:
//    Matt Cooper vtbassmatt@gmail.com
// 
//  Copyright (c) 2010, Matt Cooper
// 
//  Microsoft Public License (Ms-PL)
// 
// This license governs use of the accompanying software. If you use the
// software, you accept this license. If you do not accept the license, do not
// use the software.
// 
// 1. Definitions
// 
// The terms "reproduce," "reproduction," "derivative works," and
// "distribution" have the same meaning here as under U.S. copyright law.
// 
// A "contribution" is the original software, or any additions or changes to
// the software.
// 
// A "contributor" is any person that distributes its contribution under this
// license.
// 
// "Licensed patents" are a contributor's patent claims that read directly on
// its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the
// license conditions and limitations in section 3, each contributor grants
// you a non-exclusive, worldwide, royalty-free copyright license to
// reproduce its contribution, prepare derivative works of its contribution,
// and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the
// license conditions and limitations in section 3, each contributor grants
// you a non-exclusive, worldwide, royalty-free license under its licensed
// patents to make, have made, use, sell, offer for sale, import, and/or
// otherwise dispose of its contribution in the software or derivative works
// of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License- This license does not grant you rights to use
// any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that
// you claim are infringed by the software, your patent license from such
// contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all
// copyright, patent, trademark, and attribution notices that are present
// in the software.
// 
// (D) If you distribute any portion of the software in source code form,
// you may do so only under this license by including a complete copy of
// this license with your distribution. If you distribute any portion of the
// software in compiled or object code form, you may only do so under a
// license that complies with this license.
// 
// (E) The software is licensed "as-is." You bear the risk of using it. The
// contributors give no express warranties, guarantees or conditions. You
// may have additional consumer rights under your local laws which this
// license cannot change. To the extent permitted under your local laws,
// the contributors exclude the implied warranties of merchantability,
// fitness for a particular purpose and non-infringement.
// 

using System;

namespace BeerMath
{


	public class Hops
	{
		
		// Tinseth constants
		// http://realbeer.com/hops/research.html
		public static decimal TinsethBignessCoefficient 			=  1.65m;
		public static decimal TinsethBignessBase					=  0.000125m;
		public static decimal TinsethBoiltimeShape				= -0.04m;
		public static decimal TinsethBoiltimeMaximumUtilization	=  4.15m;
		private static decimal TinsethNonmetricMagicNumber = 7490m;

		public static decimal CalculateIbus (decimal AlphaAcid, decimal HopsOzs, decimal BoilMinutes)
		{
			// ((Alpha Acids AA% x Quantity in oz's) x % Utilization) / 7.25
			return (AlphaAcid * HopsOzs * _StandardUtilization(BoilMinutes)) / 7.25m;
		}

		public static decimal CalculateIbus (decimal AlphaAcid, decimal HopsOzs, decimal BoilMinutes, decimal Gravity, decimal WortGallons)
		{
			return CalculateIbus (AlphaAcid, HopsOzs, BoilMinutes);
		}
		
		public static decimal CalculateIbusTinseth (decimal AlphaAcid, decimal HopsOzs, decimal BoilMinutes, decimal Gravity, decimal WortGallons)
		{
			// IBUs = (Boil Time Factor * Bigness Factor) * (mg/l of added alpha acids)
			return _BoilTimeFactor(BoilMinutes) * _BignessFactor(Gravity) * _MgAlphaAcids(AlphaAcid, HopsOzs, WortGallons);
		}

		private static decimal _StandardUtilization (decimal BoilMinutes)
		{
			/*
			 * Percent Utilization Chart:
				00-05 minutes 5.0%
				06-10 minutes 6.0%
				11-15 minutes 8.0%
				16-20 minutes 10.1%
				21-25 minutes 12.1%
				26-30 minutes 15.3%
				31-35 minutes 18.8%
				34-40 minutes 22.8%
				41-45 minutes 26.9%
				46-50 minutes 28.1%
				51-60 minutes 30.0%
			*/
			if(BoilMinutes < 0)
				throw new BeerMathException("Boil time cannot be negative");
			
			if(BoilMinutes <= 5)
				return 5m;
			if(BoilMinutes <= 10)
				return 6m;
			if(BoilMinutes <= 15)
				return 8m;
			if(BoilMinutes <= 20)
				return 10.1m;
			if(BoilMinutes <= 25)
				return 12.1m;
			if(BoilMinutes <= 30)
				return 15.3m;
			if(BoilMinutes <= 35)
				return 18.8m;
			if(BoilMinutes <= 40)
				return 22.8m;
			if(BoilMinutes <= 45)
				return 26.9m;
			if(BoilMinutes <= 50)
				return 28.1m;
			if(BoilMinutes <= 60)
				return 30m;

			throw new BeerMathException("Boil times greater than 60 minutes are not supported in this version");
		}

		private static decimal _MgAlphaAcids (decimal AlphaAcid, decimal HopsOzs, decimal WortGallons)
		{
			// mg/l of added alpha acids = (decimal AA rating * oz's hops * 7490) / (volume of finished beer in gallons)
			return (AlphaAcid * HopsOzs * TinsethNonmetricMagicNumber) / WortGallons;
		}


		private static decimal _BignessFactor (decimal Gravity)
		{
			// Bigness factor = 1.65 * 0.000125^(wort gravity - 1)
			return (decimal)((double)TinsethBignessCoefficient * Math.Pow((double)TinsethBignessBase, (double)(Gravity - 1)));
		}


		private static decimal _BoilTimeFactor (decimal BoilMinutes)
		{
			// Boil Time factor =1 - e^(-0.04 * time in min's) / ( 4.15)
			return (decimal)((1 - Math.Pow(Math.E, (double)(TinsethBoiltimeShape * BoilMinutes))) / (double)TinsethBoiltimeMaximumUtilization);
		}

	}
}
