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
using NUnit.Framework;
using BeerMath;
using System.Diagnostics;

namespace Tests
{


	[TestFixture]
	public class HopsTest
	{

		[Test]
		public void IbuCase ()
		{
			decimal alpha = 6.0m;
			decimal ozs = 1.0m;
			decimal minutes = 60m;
			
			Bitterness result = Hops.CalculateIbus(alpha, ozs, minutes);

			Assert.IsAssignableFrom(typeof(BitternessType), result.Type);
			Assert.That(result.Type, Is.EqualTo(BitternessType.Ibu));
			Assert.That(result.Value, Is.AtLeast(24.82m));
			Assert.That(result.Value, Is.AtMost(24.83m));
		}
		
		[Test]
		public void IbuTinsethCase ()
		{
			decimal alpha = 6.0m;
			decimal ozs = 1.0m;
			decimal minutes = 60m;
			decimal gravity = 1.05m;
			decimal gallons = 5m;

			Bitterness result = Hops.CalculateIbusTinseth(alpha, ozs, minutes, gravity, gallons);

			Assert.IsAssignableFrom(typeof(BitternessType), result.Type);
			Assert.That(result.Type, Is.EqualTo(BitternessType.Ibu));
			Assert.That(result.Value, Is.AtLeast(20.73m));
			Assert.That(result.Value, Is.AtMost(20.74m));
		}
			
		[Test]
		public void IbuRagerCase()
		{
			decimal alpha = 6.0m;
			decimal ozs = 1.0m;
			decimal minutes = 60m;
			decimal gravity = 1.05m;
			decimal gallons = 5m;

			Bitterness result = Hops.CalculateIbusRager(alpha, ozs, gallons, gravity, minutes);

			Assert.IsAssignableFrom(typeof(BitternessType), result.Type);
			Assert.That(result.Type, Is.EqualTo(BitternessType.Ibu));
			Assert.That(result.Value, Is.AtLeast(27.59m));
			Assert.That(result.Value, Is.AtMost(27.60m));
		}

		[Test]
		public void IbuGaretzCase()
		{
			decimal alpha = 5.5m;
			decimal ozs = 1.0m;
			decimal minutes = 60m;
			decimal gravity = 1.05m;
			decimal finalVolume = 5m;
			decimal boilVolume = 6.0m;
			decimal desiredIBU = 20m;
			decimal elevation = 550m;

			Bitterness result = Hops.CalculateIbusGaretz(alpha, ozs, finalVolume, boilVolume, gravity, minutes, desiredIBU, elevation);

			Assert.IsAssignableFrom(typeof(BitternessType), result.Type);
			Assert.That(result.Type, Is.EqualTo(BitternessType.Ibu));
			Assert.That(result.Value, Is.AtMost(15.9m));
			Assert.That(result.Value, Is.AtLeast(15.8m));
		}

		[Test]
		public void BeerBalanceTest()
		{
			decimal result = Hops.CalculateBalanceRatio(10m, 40m, new Bitterness(40m, BitternessType.Ibu));

			Assert.That(result, Is.AtMost(2.09m));
			Assert.That(result, Is.AtLeast(2.07m));
		}

		[Test]
		public void HbuTest()
		{
			decimal alpha = 6.0m;
			decimal hopsOz = 1.0m;
			Bitterness result = Hops.CalculateHbus(alpha, hopsOz);

			Assert.IsAssignableFrom(typeof(BitternessType), result.Type);
			Assert.That(result.Type, Is.EqualTo(BitternessType.Hbu));
			Assert.That(result.Value, Is.EqualTo(6.0m));
		}
	}
}
