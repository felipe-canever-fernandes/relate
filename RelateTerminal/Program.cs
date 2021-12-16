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
			var mainMenu = new Menu
			(
				new List<BaseScreen>
				{
					new Screen.Screen(ListEntries, "List entries"),
					new Screen.Screen(AddEntry, "Add entry")
				},

				"Relate"
			);

			mainMenu.Display();
		}

		static void ListEntries()
		{
			var entries = Database.ReadEntries();

			if (entries.Count <= 0)
				Console.WriteLine("No entries have been added yet.");
			else
			{
				var items = new List<BaseScreen>();

				foreach (var entry in entries)
					items.Add(new Screen.Screen(() => { }, entry.Name));

				var menu = new Menu(items) { ExitLabel = "Cancel" };
				menu.Display();
			}

			_ = Console.ReadLine();
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
						"There is already an entry with that name."
					);
				}

				Console.WriteLine();
			}

			_ = Console.ReadLine();
		}
	}
}
