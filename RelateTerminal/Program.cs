using System.Collections.Generic;

namespace RelateTerminal
{
    internal class Program
    {
        static void Main()
        {
            var mainMenu = new Menu.Menu
            (
                "Relate",

                new List<Menu.Item>
                {
                    new Menu.Item("List entries"),
                    new Menu.Item("Create entry")
                }
            );

            mainMenu.Display();
        }
    }
}
