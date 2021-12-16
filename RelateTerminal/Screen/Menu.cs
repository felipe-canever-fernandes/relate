using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RelateTerminal.Menu
{
	internal class Menu
	{
		private string _title;
		private List<Item> _items;
		private string _exitLabel;

		public Menu
		(
			List<Item> items,
			string title = null,
			bool clearsScreen = true,
			string exitLabel = "Exit",
			bool displaysOnce = false
		)
		{
			Title = title;
			Items = items;
			ClearsScreen = clearsScreen;
			ExitLabel = exitLabel;
			DisplaysOnce = displaysOnce;
		}

		public string Title
		{
			get { return _title; }

			set
			{
				if (value != null)
				{
					value = value.Trim();

					Debug.Assert
					(
						value != "",
						"The menu title cannot be empty or only whitespace."
					);
				}

				_title = value;
			}
		}

		public bool ClearsScreen { get; set; }

		public string ExitLabel
		{
			get { return _exitLabel; }

			set
			{
				Debug.Assert
				(
					!string.IsNullOrEmpty(value),
					"The menu exit label cannot be null or empty."
				);

				value = value.Trim();

				Debug.Assert
				(
					value != "",
					"The menu exit label cannot be only whitespace."
				);

				_exitLabel = value;
			}
		}

		public bool DisplaysOnce { get; set; }

		public List<Item> Items
		{
			get { return _items; }

			set
			{
				Debug.Assert
				(
					value != null,
					"The list of menu items cannot be null."
				);

				Debug.Assert
				(
					value.Count > 0,
					"The list of menu items cannot be empty."
				);

				_items = value;
			}
		}

		public static void Wait()
		{
			Console.WriteLine("Press any key to continue...");
			_ = Console.ReadKey();
		}

		public void Display()
		{
			do
			{
				if (ClearsScreen)
				{
					Console.Clear();
				}

				if (Title != null)
				{
					Console.WriteLine($"\t\t{Title.ToUpper()}");
					Console.WriteLine();
				}

				for (var i = 0; i < Items.Count; ++i)
				{
					DisplayItem(i + 1, Items[i].Label);
				}

				DisplayItem(0, ExitLabel);

				Console.WriteLine();

				Console.Write("Enter an option: ");

				var isOptionValid =
						int.TryParse(Console.ReadLine(), out int option) &&
						option >= 0 && option <= Items.Count;

				if (!isOptionValid)
				{
					Console.WriteLine();
					Console.WriteLine("Invalid option.");

					Console.WriteLine();
					Wait();

					continue;
				}

				if (option == 0)
				{
					break;
				}

				Items[option - 1].Function();

				void DisplayItem(int number, string label)
				{
					Debug.Assert
					(
						number >= 0,
						"the menu item number cannot be negative"
					);

					Debug.Assert
					(
						label != null,
						"the menu item label cannot be null"
					);

					Console.WriteLine($"\t{number}. {label}");
				}
			}
			while (!DisplaysOnce);
		}
	}
}
