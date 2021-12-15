using System.Collections.Generic;

namespace RelateTerminal
{
	internal class Program
	{
		static void Main()
		{
			Screen.BaseScreen mainMenu = new Screen.Menu
			(
				"Relate",
				null
			);

			mainMenu.Display();
		}
	}
}
