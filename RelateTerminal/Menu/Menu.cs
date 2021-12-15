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

		public Menu(string title, List<Item> items, string exitLabel = "Exit")
		{
			Title = title;
			Items = items;
			ExitLabel = exitLabel;
		}

		public string Title
		{
			get { return _title; }

			set
			{
				Debug.Assert
				(
					!string.IsNullOrEmpty(value),
					"the menu title cannot be null or empty"
				);

				value = value.Trim();

				Debug.Assert
				(
					value != "",
					"the menu title cannot be only whitespace"
				);

				_title = value;
			}
		}

		public string ExitLabel
		{
			get { return _exitLabel; }

			set
			{
				Debug.Assert
				(
					!string.IsNullOrEmpty(value),
					"the menu exit label cannot be null or empty"
				);

				value = value.Trim();

				Debug.Assert
				(
					value != "",
					"the menu exit label cannot be only whitespace"
				);

				_exitLabel = value;
			}
		}

		public List<Item> Items
		{
			get { return _items; }

			set
			{
				Debug.Assert
				(
					value != null,
					"the list of menu items cannot be null"
				);

				Debug.Assert
				(
					value.Count > 0,
					"the list of menu items cannot be empty"
				);

				_items = value;
			}
		}

		public void Display()
		{
			while (true)
			{
				Console.Clear();

				Console.WriteLine($"\t\t{Title.ToUpper()}");

				Console.WriteLine();

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
					_ = Console.ReadLine();

					continue;
				}

				if (option == 0)
					break;

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
		}
	}
}
