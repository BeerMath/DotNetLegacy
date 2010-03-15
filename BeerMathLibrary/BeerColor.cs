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
	/// <summary>
	///   The various beer color calculations known to BeerMath
	/// </summary>
	public enum BeerColorType { Mcu, Srm, Ebc }

	/// <summary>
	/// Represents the color of a wort or beer
	/// </summary>
	public class BeerColor
	{
		private Decimal _Value;
		private BeerColorType _BCT;
		
		// some constants we'll use to convert between the colors
		private const double SrmFactor = 1.4922;
		private const double SrmExponent = 0.6859;
		private const decimal EcuFactor = 1.97m;
		
		/// <summary>
		/// Returns a beer color of 0 MCU
		/// </summary>
		public BeerColor ()
		{
			_Value = 0;
			_BCT = BeerColorType.Mcu;
		}
		/// <summary>
		/// Returns a beer color object with a particular color in a particular system
		/// </summary>
		/// <param name="Color">
		/// A <see cref="System.Decimal"/> magnitude of color
		/// </param>
		/// <param name="bct">
		/// A <see cref="BeerColorType"/>
		/// </param>
		public BeerColor (decimal Color, BeerColorType bct)
		{
			_Value = Color;
			_BCT = bct;
		}
		
		/// <summary>
		/// This beer color in SRM, converted if necessary
		/// </summary>
		public decimal Srm
		{
			get { return _GetSrm(); }
		}
		/// <summary>
		/// This beer color in MCU, converted if necessary
		/// </summary>
		public decimal Mcu
		{
			get { return _GetMcu(); }
		}
		/// <summary>
		/// This beer color in EBC, converted if necessary
		/// </summary>
		public decimal Ebc
		{
			get { return _GetEbc(); }
		}
		
		/// <summary>
		/// Implicit conversion to decimal
		/// </summary>
		/// <param name="b">
		/// A <see cref="BeerColor"/>
		/// </param>
		/// <returns>
		/// A <see cref="Decimal"/>
		/// </returns>
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
