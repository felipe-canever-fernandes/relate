using System;
using System.Diagnostics;

namespace RelateTerminal
{
    internal class Menu
    {
        private string _title;

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

        public Menu(string title) => Title = title;
        public void Display() => Console.WriteLine($"\t{Title.ToUpper()}");
    }
}
