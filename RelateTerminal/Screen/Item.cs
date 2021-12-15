using System.Diagnostics;

namespace RelateTerminal.Screen
{
	internal class Item
	{
		private string _label;

		public Item(string label) => Label = label;

		public string Label
		{
			get { return _label; }

			set
			{
				Debug.Assert
				(
					!string.IsNullOrEmpty(value),
					"the menu item label cannot be null or empty"
				);

				value = value.Trim();

				Debug.Assert
				(
					value != "",
					"the menu item label cannot be only whitespace"
				);

				_label = value;
			}
		}
	}
}
