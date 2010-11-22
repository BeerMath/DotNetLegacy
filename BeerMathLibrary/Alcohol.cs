// 
//  Author:
//    mattc 
// 
//  Copyright (c) 2010, mattc
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

	/// <summary>
	/// Helper class for alcohol calculations
	/// </summary>
	public sealed class Alcohol
	{
		private const decimal AbwMagicNumber = 0.79m;

		#region Calorie Calculation constants
		public const decimal CalorieCalculationAbwMultiplier			= 6.9m;
		public const decimal CalorieCalculationAbwRealExtractSum		= 4.0m;
		public const decimal CalorieCalculationRealExtractSubtraction	= 0.1m;
		public const decimal CalorieCalculationFinalGravityMultiplier	= 3.55m;
		#endregion


		/// <summary>
		/// Calculates ABV (alcohol by volume) for a wort
		/// </summary>
		/// <param name="OriginalGravity">
		/// A <see cref="Gravity"/>
		/// </param>
		/// <param name="FinalGravity">
		/// A <see cref="Gravity"/>
		/// </param>
		/// <returns>
		/// A <see cref="Abv"/>
		/// </returns>
		public static Abv CalculateAbv(Gravity OriginalGravity, Gravity FinalGravity)
		{
			return new Abv((OriginalGravity.Value - FinalGravity.Value) * _AbvFactor(OriginalGravity.Value - FinalGravity.Value));
		}
		
		/// <summary>
		/// Calculates ABW (alcohol by weight) for a wort
		/// </summary>
		/// <param name="OriginalGravity">
		/// A <see cref="Gravity"/>
		/// </param>
		/// <param name="FinalGravity">
		/// A <see cref="Gravity"/>
		/// </param>
		/// <returns>
		/// A <see cref="Abw"/>
		/// </returns>
		public static Abw CalculateAbw (Gravity OriginalGravity, Gravity FinalGravity)
		{
			return new Abw((AbwMagicNumber * CalculateAbv(OriginalGravity, FinalGravity)) / FinalGravity.Value);
		}
		
		private static decimal _AbvFactor(decimal GravityDifference)
		{
			/* 
			   Grav diff       Factor
			0.0000 - 0.0069		125
			0.0070 – 0.0104		126
			0.0105 – 0.0172		127
			0.0173 – 0.0261		128
			0.0262 – 0.0360		129
			0.0361 – 0.0465		130
			0.0466 – 0.0571		131
			0.0572 – 0.0679		132
			0.0680 – 0.0788		133
			0.0789 – 0.0897		134
			0.0898 – 0.1007		135
			
			source: http://www.hmrc.gov.uk/manuals/beerpolmanual/BEERPOL12030.htm
			 */
			if(GravityDifference < 0)
				throw new BeerMathException("Gravity difference cannot be negative");
			
			if(GravityDifference <= 0.0069m)
				return 125m;
			if(GravityDifference <= 0.0104m)
				return 126m;
			if(GravityDifference <= 0.0172m)
				return 127m;
			if(GravityDifference <= 0.0261m)
				return 128m;
			if(GravityDifference <= 0.0360m)
				return 129m;
			if(GravityDifference <= 0.0465m)
				return 130m;
			if(GravityDifference <= 0.0571m)
				return 131m;
			if(GravityDifference <= 0.0679m)
				return 132m;
			if(GravityDifference <= 0.0788m)
				return 133m;
			if(GravityDifference <= 0.0897m)
				return 134m;
			if(GravityDifference <= 0.1007m)
				return 135m;
			
			throw new BeerMathException("Gravity differences greater than 0.1007 are not supported in this version");
		}

		/// <summary>
		/// Calculates the estimated caloric content of the batch based on its Gravity.
		/// </summary>
		/// <param name="OriginalGravity">
		/// The original <see cref="Gravity"/> contribution of the grain.
		/// </param>
		/// <param name="FinalGravity">
		/// The final <see cref="Gravity"/> contribution of the grain.
		/// </param>
		/// <returns>
		/// A <see cref="System.Decimal"/> representing the batch's caloric content.
		/// </returns>
		public static decimal CalculateCalories(Gravity OriginalGravity, Gravity FinalGravity)
		{
			Abw abw = CalculateAbw(OriginalGravity, FinalGravity);
			Gravity realExtract = Malt.CalculateRealExtract(OriginalGravity, FinalGravity);
			return ((CalorieCalculationAbwMultiplier * abw) 
						+ (CalorieCalculationAbwRealExtractSum * (realExtract.Plato - CalorieCalculationRealExtractSubtraction))) 
					* FinalGravity.Value * CalorieCalculationFinalGravityMultiplier;
		}
	}
}
