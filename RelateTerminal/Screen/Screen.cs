﻿using System.Diagnostics;

namespace RelateTerminal.Screen
{
	internal abstract class Screen
	{
		private string _title;

		public Screen(string title) => Title = title;

		public string Title
		{
			get { return _title; }

			set
			{
				Debug.Assert
				(
					!string.IsNullOrEmpty(value),
					"the screen title cannot be null or empty"
				);

				value = value.Trim();

				Debug.Assert
				(
					value != "",
					"the screen title cannot be only whitespace"
				);

				_title = value;
			}
		}

		public abstract void Display();
	}
}
