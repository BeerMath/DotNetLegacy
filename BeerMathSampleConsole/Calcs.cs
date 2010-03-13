
using System;
using BeerMath;

namespace BeerMath.Sample.Console
{


	public class Calcs
	{

		public Calcs ()
		{
		}
		
		private static decimal PromptDecimal(string text)
		{
			System.Console.Write(text);
			System.Console.Write(" >");
			return decimal.Parse(System.Console.ReadLine());
		}
		
		public static void McuTest()
		{
			decimal lbsGrain = PromptDecimal("Pounds of grain");
        		decimal degLovibond = PromptDecimal("Degrees Lovibond");
        		decimal totalVolume = PromptDecimal("Total volume");

            decimal MCUs = Malt.CalculateMcu(lbsGrain, degLovibond, totalVolume);

		    System.Console.WriteLine(String.Format("MCUs = {0}", MCUs));
		}

		public static void SrmTest()
		{
			decimal lbsGrain = PromptDecimal("Pounds of grain");
        		decimal degLovibond = PromptDecimal("Degrees Lovibond");
        		decimal totalVolume = PromptDecimal("Total volume");

            decimal SRM = Malt.CalculateSrm(lbsGrain, degLovibond, totalVolume);

		    System.Console.WriteLine(String.Format("SRM = {0}", SRM));
		}

		public static void EbcTest()
		{
			decimal lbsGrain = PromptDecimal("Pounds of grain");
        		decimal degLovibond = PromptDecimal("Degrees Lovibond");
        		decimal totalVolume = PromptDecimal("Total volume");

            decimal EBC = Malt.CalculateEbc(lbsGrain, degLovibond, totalVolume);

		    System.Console.WriteLine(String.Format("EBC = {0}", EBC));
		}
	}
}
