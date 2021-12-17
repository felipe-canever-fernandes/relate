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
					new Item("Search entry", SearchEntry),
					new Item("Add entry", AddEntry),
					new Item("List entries", ListAllEntries)
				}
			);

			menu.Display(out _);
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

		static void ListAllEntries()
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

				var items = new List<Item>();

				var relatedEntries = Database.ReadRelatedEntries(entry.Id);

				foreach (var relatedEntry in relatedEntries)
				{
					items.Add
					(
						new Item
						(
							relatedEntry.ToString(),
							() => DisplayEntry(relatedEntry.Id)
						)
					);
				}

				items.Add
				(
					new Item("Create relation", () => CreateRelation(entry.Id))
				);

				items.Add
				(
					new Item
					(
						"Destroy relation",
						() => DestroyRelation(entry.Id)
					)
				);

				items.Add(new Item("Rename", () => RenameEntry(entry.Id)));
				items.Add(new Item("Delete", () => DeleteEntry(entry.Id)));

				var menu = new Menu.Menu
				(
					items: items,

					title: entry.ToString(),
					exitLabel: "Go back",
					displaysOnce: true
				);

				menu.Display(out bool exited);

				if (exited)
					break; 
			}
		}

		static void CreateRelation(long entryId)
		{
			Debug.Assert
			(
				entryId >= 1,
				"The entry ID must be positive."
			);

			while (true)
			{
				var entry = Database.ReadEntry(entryId);

				Console.Clear();

				Console.WriteLine($"\tCreate relation for {entry}");
				Console.WriteLine();

				var relatableEntries = Database.ReadRelatableEntries(entry.Id);

				if (relatableEntries.Count <= 0)
				{
					Console.WriteLine
					(
						"There are no entries " +
						$"which {entry} can be related to."
					);

					break;
				}

				var items = new List<Item>();

				foreach (var relatableEntry in relatableEntries)
				{
					items.Add
					(
						new Item
						(
							relatableEntry.ToString(),
							() => CreateRelation(entry, relatableEntry)
						)
					);
				}

				var menu = new Menu.Menu
				(
					items: items,
					clearsScreen: false,
					exitLabel: "Cancel",
					displaysOnce: true
				);

				menu.Display(out bool exited);

				if (exited)
				{
					return;
				}
			}

			Console.WriteLine();
			Menu.Menu.Wait();

			void CreateRelation(Entry firstEntry, Entry secondEntry)
			{
				var successful = Database.Create
				(
					new Relation(firstEntry.Id, secondEntry.Id)
				);

				Console.WriteLine();

				if (successful)
				{
					Console.WriteLine
					(
						$"Relation {firstEntry}-{secondEntry} " +
						"successfully created."
					);
				}
				else
				{
					Console.WriteLine("The relation could not be created.");
				}

				Console.WriteLine();
				Menu.Menu.Wait();
			}
		}

		static void DestroyRelation(long entryId)
		{
			Debug.Assert
			(
				entryId >= 1,
				"The entry ID must be positive."
			);

			while (true)
			{
				var entry = Database.ReadEntry(entryId);

				Console.Clear();

				Console.WriteLine($"\tDestroy relation of {entry}");
				Console.WriteLine();

				var relatedEntries = Database.ReadRelatedEntries(entry.Id);

				if (relatedEntries.Count <= 0)
				{
					Console.WriteLine
					(
						$"{entry} is not related to any other entries."
					);

					break;
				}

				var items = new List<Item>();

				foreach (var relatedEntry in relatedEntries)
				{
					items.Add
					(
						new Item
						(
							relatedEntry.ToString(),
							() => DestroyRelation(entry, relatedEntry)
						)
					);
				}

				var menu = new Menu.Menu
				(
					items: items,
					clearsScreen: false,
					exitLabel: "Go back",
					displaysOnce: true
				);

				menu.Display(out bool exited);

				if (exited)
				{
					return;
				}
			}

			Console.WriteLine();
			Menu.Menu.Wait();

			void DestroyRelation(Entry firstEntry, Entry secondEntry)
			{
				var successful = Database.Delete
				(
					new Relation(firstEntry.Id, secondEntry.Id)
				);

				Console.WriteLine();

				if (successful)
				{
					Console.WriteLine
					(
						$"Relation {firstEntry}-{secondEntry} " +
						"successfully destroyed."
					);
				}
				else
				{
					Console.WriteLine("The relation could not be destroyed.");
				}

				Console.WriteLine();
				Menu.Menu.Wait();
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
