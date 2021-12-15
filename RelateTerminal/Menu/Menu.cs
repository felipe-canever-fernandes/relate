using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RelateTerminal.Menu
{
    internal class Menu
    {
        private string _title;
        private List<Item> _items;

        public Menu(string title, List<Item> items)
        {
            Title = title;
            Items = items;
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
                Console.WriteLine($"\t\t{Title.ToUpper()}");

                Console.WriteLine();

                for (var i = 0; i < Items.Count; ++i)
                {
                    DisplayItem(i + 1, Items[i].Label);
                }

                DisplayItem(0, "Exit");

                Console.WriteLine();

                Console.Write("Enter an option: ");

                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid option.");
					_ = Console.ReadLine();

                    continue;
                }

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
