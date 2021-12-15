using RelateLibrary;
using RelateLibrary.Database;

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
			while (true)
			{
				Console.Write("Name: ");
				var name = Console.ReadLine().Trim();

				if (name == "")
					break;

				try
				{
					if (Database.Create(new Entry(name)))
						Console.WriteLine("Entry successfully added.");
					else
						Console.WriteLine("The entry could not be added.");

					break;
				}
				catch (NotUniqueException)
				{
					Console.WriteLine
					(
						"There already is an entry with that name."
					);
				}

				Console.WriteLine();
			}

			_ = Console.ReadLine();
		}
	}
}
