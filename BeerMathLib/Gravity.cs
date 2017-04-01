// 
//  Author:
//    Ryan Muzzey
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
	/// Represents the gravity
	/// </summary>
	public sealed class Gravity
	{
		private decimal _Points;
		private decimal _Value;
		private decimal _Plato;

		#region Singleton for Zero Value.

		private Gravity()
		{
			_Value = 0m;
			_Points = 0m;
			_Plato = GetPlato();
		}

		private static Gravity _Zero = new Gravity();
		/// <summary>
		/// A static Gravity value of 0.
		/// </summary>
		public static Gravity Zero
		{
			get
			{
				return _Zero;
			}
		}

		#endregion

		/// <summary>
		/// A constructor for a new Gravity.
		/// </summary>
		/// <param name="Points">
		/// A <see cref="System.Decimal"/> representing the points value (40 instead of 1.040) of the Gravity.
		/// </param>
		public Gravity(decimal Points)
		{
			_Points = Points;
			_Value = (_Points / 1000m) + 1;
			_Plato = GetPlato();
		}

		private decimal GetPlato()
		{
			return (-463.37m) + (668.72m * _Value) - (205.35m * (_Value * _Value));
		}

		/// <summary>
		/// Helper method for converting Plato to a Points value.
		/// </summary>
		/// <param name="Plato">
		/// A <see cref="System.Decimal"/> representing the Plato value.
		/// </param>
		/// <returns>
		/// A <see cref="System.Decimal"/> representing the Points.
		/// </returns>
		public static decimal ConvertPlatoToPoints(decimal Plato)
		{
			return (ConvertPlatoToValue(Plato) - 1) * 1000;
		}

		/// <summary>
		/// Helper method for converting Plato to a <see cref="Gravity"/> Value.
		/// </summary>
		/// <param name="Plato">
		/// A <see cref="System.Decimal"/> representing the Plato value.
		/// </param>
		/// <returns>
		/// A <see cref="System.Decimal"/> representing the Value.
		/// </returns>
		public static decimal ConvertPlatoToValue(decimal Plato)
		{
			return (Plato / (258.6m - ((Plato / 258.5m) * 227.1m))) + 1;
		}

		/// <summary>
		/// Gets the Points of the Gravity as a whole number. Like 40.
		/// </summary>
		public decimal Points
		{
			get
			{
				return _Points;
			}
		}

		/// <summary>
		/// Gets the Value of the Gravity. Like 1.040.
		/// </summary>
		public decimal Value
		{
			get
			{
				return _Value;
			}
		}

		/// <summary>
		/// Gets the Plato of the Gravity. 
		/// Calculated during construction by this method http://hbd.org/ensmingr/ (Equation 1).
		/// </summary>
		public decimal Plato
		{
			get
			{
				return _Plato;
			}
		}

		/// <summary>
		/// Implicit conversion to decimal based on the Points.
		/// </summary>
		/// <param name="gravity">
		/// A <see cref="Gravity"/>.
		/// </param>
		/// <returns>
		/// A <see cref="System.Decimal"/>
		/// </returns>
		public static implicit operator decimal(Gravity gravity)
		{
			return gravity._Points;
		}

		public override string ToString()
		{
			return string.Format("{0} {1}", _Points, _Value);
		}
	}
}
