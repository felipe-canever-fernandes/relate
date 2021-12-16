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
					new Item("Search entry", SearchEntry),
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

				Console.WriteLine("\tAll entries");
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
							entry.ToString(),
							() => DisplayEntry(entry.Id)
						)
					);
				}

				var menu = new Menu.Menu
				(
					items,
					clearsScreen: false,
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

		static void SearchEntry()
		{
			Console.Clear();

			Console.WriteLine("\tSearch entry");
			Console.WriteLine();

			Console.Write("Search: ");
			var search = Console.ReadLine().Trim();

			if (search == "")
			{
				return;
			}

			while (true)
			{
				var entries = Database.SearchEntry(search);

				if (entries.Count <= 0)
				{
					Console.WriteLine();
					Console.WriteLine("No entries match your search.");

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
							entry.ToString(),
							() => DisplayEntry(entry.Id)
						)
					);
				}

				var menu = new Menu.Menu
				(
					items,
					title: $"Search results for \"{search}\"",
					clearsScreen: true,
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

			Console.WriteLine("\tAdd entry");
			Console.WriteLine();

			long id;

			while (true)
			{
				Console.Write("Name: ");
				var name = Console.ReadLine().Trim();

				if (name == "")
				{
					return;
				}

				try
				{
					id = Database.Create(new Entry(name));

					if (id > 0)
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

			if (id > 0)
			{
				DisplayEntry(id);
			}
		}

		static void DisplayEntry(long id)
		{
			Debug.Assert
			(
				id >= 1,
				"The entry ID must be positive."
			);

			while (true)
			{
				var entry = Database.ReadEntry(id);

				if (entry == null)
				{
					return;
				}

				var menu = new Menu.Menu
				(
					items: new List<Item>
					{
						new Item("Rename", () => RenameEntry(entry.Id)),
						new Item("Delete", () => DeleteEntry(entry.Id))
					},

					title: entry.ToString(),
					exitLabel: "Go back",
					displaysOnce: true
				);

				menu.Display(out bool exited);

				if (exited)
					break; 
			}
		}

		static void RenameEntry(long id)
		{
			Debug.Assert
			(
				id >= 1,
				"The entry ID must be positive."
			);

			var entry = Database.ReadEntry(id);

			Console.Clear();

			Console.WriteLine($"\tRename {entry}");
			Console.WriteLine();

			while (true)
			{
				Console.Write("New name: ");
				entry.Name = Console.ReadLine().Trim();

				if (entry.Name == "")
				{
					break;
				}

				try
				{
					if (Database.Update(entry))
					{
						Console.WriteLine("Entry successfully renamed.");
					}
					else
					{
						Console.WriteLine("The entry could not be renamed.");
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

		static void DeleteEntry(long id)
		{
			Debug.Assert
			(
				id >= 1,
				"The entry ID must be positive."
			);

			var entry = Database.ReadEntry(id);

			Console.Clear();

			Console.WriteLine($"\tDelete {entry}");
			Console.WriteLine();

			Console.Write
			(
				"Are you sure you want to delete this entry? (y/n): "
			);

			var answer = Console.ReadLine().ToLower();

			if (answer == "y")
			{
				Console.WriteLine();

				if (Database.Delete(entry))
				{
					Console.WriteLine("Entry successfully deleted.");
				}
				else
				{
					Console.WriteLine("The entry could not be deleted.");
				}
			}

			Console.WriteLine();
			Menu.Menu.Wait();
		}
	}
}
