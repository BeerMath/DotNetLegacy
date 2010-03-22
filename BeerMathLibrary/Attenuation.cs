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


	public class Attenuation
	{
		private const decimal RealFactorMagicNumber = 0.81m;
		/// <summary>
		/// Attenuation is a measure of how much of the sugar was fermented by the yeast.  Apparent attenuation is the unadjusted
		/// percent of sugars fermented by the yeast.  For beer brewing, apparent attenuation is much more commonly used than real
		/// attenuation.
		/// </summary>
		/// <param name="OriginalGravity">
		/// A <see cref="System.Decimal"/>
		/// </param>
		/// <param name="FinalGravity">
		/// A <see cref="System.Decimal"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Decimal"/>
		/// </returns>
		public static decimal CalculateApparent (decimal OriginalGravity, decimal FinalGravity)
		{
			//Apparent Attenuation % = ((OG-1)-(FG-1)) / (OG-1) x 100
			return ((OriginalGravity-1)-(FinalGravity-1)) / (OriginalGravity-1)*100;
		}

		/// <summary>
		/// The real attenuation is how much sugars were really fermented by the yeast.  Because alcohol is lighter in specific
		/// gravity than water, an adjustment must be made for the alcohol content when assessing the actual percentages of sugar
		/// fermented.  The real attenuation will always be a lower number than the apparent attenuation.
		/// </summary>
		/// <param name="OriginalGravity">
		/// A <see cref="System.Decimal"/>
		/// </param>
		/// <param name="FinalGravity">
		/// A <see cref="System.Decimal"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Decimal"/>
		/// </returns>
		public static decimal CalculateReal (decimal OriginalGravity, decimal FinalGravity)
		{
			//Real Attenuation = Apparent Attenuation * 0.81
			return CalculateApparent(OriginalGravity, FinalGravity) * RealFactorMagicNumber;
		}
	}
}
