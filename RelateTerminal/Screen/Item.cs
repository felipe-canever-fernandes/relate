using System.Diagnostics;

namespace RelateTerminal.Screen
{
	internal class Item
	{
		private string _label;
		private FunctionCallback _function;

		public Item(string label, FunctionCallback function)
		{
			Label = label;
			Function = function;
		}

		public delegate void FunctionCallback();

		public string Label
		{
			get { return _label; }

			set
			{
				Debug.Assert
				(
					!string.IsNullOrEmpty(value),
					"The menu item label cannot be null or empty."
				);

				value = value.Trim();

				Debug.Assert
				(
					value != "",
					"The menu item label cannot be only whitespace."
				);

				_label = value;
			}
		}

		public FunctionCallback Function
		{
			get { return _function; }

			set
			{
				Debug.Assert
				(
					value != null,
					"The menu item function cannot be null."
				);

				_function = value;
			}
		}
	}
}
