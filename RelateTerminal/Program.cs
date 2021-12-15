using System.Collections.Generic;

namespace RelateTerminal
{
	internal class Program
	{
		static void Main()
		{
			var mainMenu = new Screen.Menu
			(
				"Relate",

				new List<Screen.Item>
				{
					new Screen.Item("List entries")
				}
			);

			mainMenu.Display();
		}
	}
}
