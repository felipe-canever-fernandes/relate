﻿using System;
using System.Diagnostics;

namespace RelateTerminal.Screen
{
	internal abstract class BaseScreen
	{
		private string _title;

		public BaseScreen(string title, bool clearsScreen = true)
		{
			Title = title;
			ClearsScreen = clearsScreen;
		}

		public string Title
		{
			get { return _title; }

			set
			{
				Debug.Assert
				(
					!string.IsNullOrEmpty(value),
					"The screen title cannot be null or empty."
				);

				value = value.Trim();

				Debug.Assert
				(
					value != "",
					"The screen title cannot be only whitespace."
				);

				_title = value;
			}
		}

		public bool ClearsScreen { get; set; }

		public virtual void Display()
		{
			if (ClearsScreen)
				Console.Clear();

			Console.WriteLine($"\t\t{Title.ToUpper()}");
			Console.WriteLine();
		}
	}
}
