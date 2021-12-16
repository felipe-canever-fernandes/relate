using System.Diagnostics;

namespace RelateTerminal.Screen
{
	internal abstract class BaseScreen
	{
		private string _title;

		public BaseScreen(string title = null) => Title = title;

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

		public abstract void Display();
	}
}
