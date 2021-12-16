using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RelateTerminal.Screen
{
	internal class Menu : BaseScreen
	{
		private List<BaseScreen> _items;
		private string _exitLabel;

		public Menu
		(
			string title,
			List<BaseScreen> items,
			bool clearsScreen = true,
			bool displaysTitle = true,
			string exitLabel = "Exit"
		) :
			base(title, clearsScreen, displaysTitle)
		{
			Items = items;
			ExitLabel = exitLabel;
		}

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

		public List<BaseScreen> Items
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

		public override void Display()
		{
			while (true)
			{
				base.Display();

				for (var i = 0; i < Items.Count; ++i)
				{
					DisplayItem(i + 1, Items[i].Title);
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

				Items[option - 1].Display();

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
