using System;

namespace BeerMath.Sample.Console
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			System.Console.WriteLine("Hello beer.");
			int Choice = -1;
			while(Choice != 0)
			{
				Menu();
				Prompt();
				Choice = ReadAnswer();
				Gosub(Choice);
				System.Console.WriteLine("-------------");
			}
		}
		
		public static void Menu()
		{
			System.Console.WriteLine("1 - MCUs");
			System.Console.WriteLine("0 - EXIT");
		}
		public static void Gosub(int choice)
		{
			switch(choice)
			{
			case 1 :
				Calcs.McuTest();
				break;
			default:
				break;
			}
		}
		
		public static void Prompt()
		{
			System.Console.Write(">");
		}
		
		public static int ReadAnswer()
		{
			string entry = System.Console.ReadLine();
			int answer = Int32.Parse(entry);
			if (answer > 1)
			{
				answer = 0;
			}
			
			return answer;
		}
	}
}
