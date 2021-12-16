using RelateLibrary;
using RelateLibrary.Database;

using RelateTerminal.Menu;

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RelateTerminal
{
	internal class Program
	{
		static void Main()
		{
			var menu = new Menu.Menu
			(
				title: "Relate",

				items: new List<Item>
				{
					new Item("List entries", function: ListEntries),
					new Item("Add entry", AddEntry)
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
			
			var items = new List<Item>();

			foreach (var entry in entries)
				items.Add(new Item(entry.Name, () => DisplayEntry(entry)));

			var menu = new Menu.Menu
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

			var menu = new Menu.Menu
			(
				title: entry.Name,

				items: new List<Item>
				{
					new Item("Rename", () => { }),
					new Item("Delete", () => { })
				},

				clearsScreen: false,
				displaysTitle: false,
				exitLabel: "Cancel"
			);

			menu.Display();
		}
	}
}
