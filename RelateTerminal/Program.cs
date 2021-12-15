using System.Collections.Generic;

namespace RelateTerminal
{
	internal class Program
	{
		static void Main()
		{
			Screen.Screen mainMenu = new Screen.Menu
			(
				"Relate",
				null
			);

			mainMenu.Display();
		}
	}
}
