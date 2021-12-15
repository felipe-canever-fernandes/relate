using RelateLibrary;
using RelateTerminal.Screen;

using System;
using System.Collections.Generic;

namespace RelateTerminal
{
	internal class Program
	{
		static void Main()
		{
			BaseScreen mainMenu = new Menu
			(
				"Relate",
				
				new List<BaseScreen>
				{
					new Screen.Screen("Add entry", AddEntry)
				}
			);

			mainMenu.Display();
		}

		static void AddEntry()
		{
			Console.Write("Name: ");
			var name = Console.ReadLine().Trim();

			if (name == "")
				return;

			Database.Create(new Entry(name));
		}
	}
}
