using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RelateTerminal.Screen
{
	internal class Screen : BaseScreen
	{
		public Screen(string title) : base(title) {}

		public override void Display()
		{
			Console.Clear();
			Console.WriteLine($"\t\t{Title.ToUpper()}");
			Console.WriteLine();
			_ = Console.ReadLine();
		}
	}
}
