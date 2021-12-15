using System;
using System.Diagnostics;

namespace RelateTerminal.Screen
{
	internal class Screen : BaseScreen
	{
		private FunctionalityCallback _functionality;

		public Screen(string title, FunctionalityCallback functionality) :
			base(title)
			=> Functionality = functionality;

		public delegate void FunctionalityCallback();

		public FunctionalityCallback Functionality
		{
			get { return _functionality; }

			set
			{
				Debug.Assert
				(
					value != null,
					"The screen functionality cannot be null."
				);

				_functionality = value;
			}
		}

		public override void Display()
		{
			Console.Clear();
			Console.WriteLine($"\t\t{Title.ToUpper()}");
			Console.WriteLine();

			Functionality();
		}
	}
}
