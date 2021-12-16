using System;
using System.Diagnostics;

namespace RelateTerminal.Screen
{
	internal abstract class BaseScreen
	{
		private string _title;

		public BaseScreen(string title = null, bool clearsScreen = true)
		{
			Title = title;
			ClearsScreen = clearsScreen;
		}

		public string Title
		{
			get { return _title; }

			set
			{
				if (value != null)
				{
					value = value.Trim();

					Debug.Assert
					(
						value != "",
						"The screen title cannot be only whitespace."
					);
				}

				_title = value;
			}
		}

		public bool ClearsScreen { get; set; }

		public virtual void Display()
		{
			if (ClearsScreen)
				Console.Clear();

			if (Title != null)
			{
				Console.WriteLine($"\t\t{Title.ToUpper()}");
				Console.WriteLine();
			}
		}
	}
}
