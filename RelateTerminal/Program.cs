using RelateTerminal.Screen;
using System.Collections.Generic;

namespace RelateTerminal
{
	internal class Program
	{
		static void Main()
		{
			BaseScreen mainMenu = new Menu
			(
				"Relate",
				
				new List<BaseScreen>
				{
					new Screen.Screen("Add entry", () => { })
				}
			);

			mainMenu.Display();
		}
	}
}
