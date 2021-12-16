using RelateLibrary;
using RelateLibrary.Database;

using RelateTerminal.Screen;

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RelateTerminal
{
	internal class Program
	{
		static void Main()
		{
			var menu = new Menu
			(
				title: "Relate",

				items: new List<BaseScreen>
				{
					new Screen.Screen
					(
						title: "List entries",
						functionality: ListEntries
					),

					new Screen.Screen
					(
						title: "Add entry",
						functionality: AddEntry
					)
				}
			);

			menu.Display();
		}

		static void ListEntries()
		{
			var entries = Database.ReadEntries();

			if (entries.Count <= 0)
			{
				Console.WriteLine("No entries have been added yet.");
				_ = Console.ReadLine();

				return;
			}
			
			var items = new List<BaseScreen>();

			foreach (var entry in entries)
				items.Add
				(
					new Screen.Screen
					(
						title: entry.Name,
						functionality: () => DisplayEntry(entry)
					)
				);

			var menu = new Menu
			(
				title: "List entries",
				items,
				exitLabel: "Cancel"
			);

			menu.Display();
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

		static void DisplayEntry(Entry entry)
		{
			Debug.Assert(entry != null);

			var menu = new Menu
			(
				title: entry.Name,

				items: new List<BaseScreen>
				{
					new Screen.Screen
					(
						title: "Rename",
						functionality: () => { }
					),

					new Screen.Screen
					(
						title: "Delete",
						functionality: () => { }
					)
				},

				clearsScreen: false,
				displaysTitle: false,
				exitLabel: "Cancel"
			);

			menu.Display();
		}
	}
}
