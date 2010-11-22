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

using BeerMath;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class GravityTest
	{
		[Test]
		public void OriginalGravityTest()
		{
			decimal grainLbs = 9.3125m; // 9 lbs 5 oz
			decimal extractPPG = 37m;
			decimal extractEfficiency = 0.75m;
			decimal volume = 5m;

			Gravity results = Malt.CalculateOriginalGravity(grainLbs, extractPPG, extractEfficiency, volume);

			Assert.That(results, Is.InstanceOf(typeof(Gravity)));
			Assert.That(results.Points, Is.AtMost(52m));
			Assert.That(results.Points, Is.AtLeast(51m));
		}

		[Test]
		public void FinalGravityTest()
		{
			decimal grainLbs = 9.3125m; // 9 lbs 5 oz
			decimal extractPPG = 37m;
			decimal extractEfficiency = 0.75m;
			decimal volume = 5m;
			decimal apparentAttenuation = 0.79m;

			Gravity result = Malt.CalculateFinalGravity(grainLbs, extractPPG, extractEfficiency, volume, apparentAttenuation);

			Assert.That(result, Is.InstanceOf(typeof(Gravity)));
			Assert.That(result.Points, Is.AtMost(11m));
			Assert.That(result.Points, Is.AtLeast(10m));
		}

		[Test]
		public void ConvertPlatoToValueTest()
		{
			decimal plato = 10m;
			decimal result = Gravity.ConvertPlatoToValue(plato);
			Assert.That(result, Is.AtMost(1.041m));
			Assert.That(result, Is.AtLeast(1.040m));
		}

		[Test]
		public void ConvertPlatoToContributionValueTest()
		{
			decimal plato = 10m;
			decimal result = Gravity.ConvertPlatoToPoints(plato);
			Assert.That(result, Is.AtMost(40.1m));
			Assert.That(result, Is.AtLeast(40m));
		}

		[Test]
		public void RealExtractTest()
		{
			Gravity originalGravity = new Gravity(70m);
			Gravity finalGravity = new Gravity(15m);

			Gravity result = Malt.CalculateRealExtract(originalGravity, finalGravity);
			
			Assert.That(result.Plato, Is.AtMost(6.22m));
			Assert.That(result.Plato, Is.AtLeast(6.21m));
		}
	}
}
