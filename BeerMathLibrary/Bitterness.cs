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
	/// The various bitterness calculations known to BeerMath
	/// </summary>
	public enum BitternessType
	{
		Ibu
	}

	/// <summary>
	/// Represents the bitterness of a wort or beer
	/// </summary>
	public sealed class Bitterness
	{

		private decimal _Value;
		private BitternessType _Type;
		
		/// <summary>
		/// Returns a bitterness of 0 IBU
		/// </summary>
		public Bitterness ()
		{
			_Value = 0;
			_Type = BitternessType.Ibu;
		}
		
		/// <summary>
		/// Creates a bitterness object with a particular value in a particular system
		/// </summary>
		/// <param name="Value">
		/// A <see cref="System.Decimal"/> magnitude of bitterness
		/// </param>
		/// <param name="Type">
		/// A <see cref="BitternessType"/>
		/// </param>
		public Bitterness (decimal Value, BitternessType Type)
		{
			_Value = Value;
			_Type = Type;
		}
		
		/// <summary>
		/// Gets the bitterness value in IBUs (International Bitterness Units)
		/// </summary>
		public decimal Ibu
		{
			get
			{
				return _Value;
			}
		}
		
		/// <summary>
		/// Implicit conversion to decimal
		/// </summary>
		/// <param name="b">
		/// A <see cref="Bitterness"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Decimal"/>
		/// </returns>
		public static implicit operator decimal(Bitterness b)
		{
			return b._Value;
		}
		
		public override string ToString ()
		{
			return string.Format("{0} {1}", _Value, _Type);
		}

	}
}
