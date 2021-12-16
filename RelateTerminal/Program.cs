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
				title: "RELATE",

				items: new List<Item>
				{
					new Item("List entries", ListEntries),
					new Item("Add entry", AddEntry)
				}
			);

			menu.Display(out _);
		}

		static void ListEntries()
		{
			while (true)
			{
				Console.Clear();

				Console.WriteLine("\tALL ENTRIES");
				Console.WriteLine();

				var entries = Database.ReadEntries();

				if (entries.Count <= 0)
				{
					Console.WriteLine("No entries have been added yet.");

					Console.WriteLine();
					Menu.Menu.Wait();

					return;
				}

				var items = new List<Item>();

				foreach (var entry in entries)
				{
					items.Add
					(
						new Item
						(
							BetweenSquareBrackets(entry.Name),
							() => DisplayEntry(entry)
						)
					);
				}

				var menu = new Menu.Menu
				(
					items,
					title: "List entries",
					exitLabel: "Go back",
					displaysOnce: true
				);

				menu.Display(out bool exited);

				if (exited)
				{
					break;
				}
			}
		}

		static void AddEntry()
		{
			Console.Clear();

			Console.WriteLine("\tADD ENTRY");
			Console.WriteLine();

			while (true)
			{
				Console.Write("Name: ");
				var name = Console.ReadLine().Trim();

				if (name == "")
				{
					break;
				}

				try
				{
					if (Database.Create(new Entry(name)))
					{
						Console.WriteLine("Entry successfully added.");
					}
					else
					{
						Console.WriteLine("The entry could not be added.");
					}

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

			Console.WriteLine();
			Menu.Menu.Wait();
		}

		static void DisplayEntry(Entry entry)
		{
			Debug.Assert(entry != null);

			var menu = new Menu.Menu
			(
				items: new List<Item>
				{
					new Item("Rename", () => { }),
					new Item("Delete", () => { })
				},

				title: BetweenSquareBrackets(entry.Name),
				exitLabel: "Go back"
			);

			menu.Display(out _);
		}

		static string BetweenSquareBrackets(string value)
		{
			Debug.Assert
			(
				value != null,
				"the string cannot be null"
			);

			return $"[{value}]";
		}
	}
}
