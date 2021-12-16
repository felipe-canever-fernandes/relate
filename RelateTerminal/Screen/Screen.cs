using System;
using System.Diagnostics;

namespace RelateTerminal.Screen
{
	internal class Screen : BaseScreen
	{
		private FunctionalityCallback _functionality;

		public Screen
		(
			string title,
			FunctionalityCallback functionality,
			bool clearsScreen = true,
			bool displaysTitle = true
		) :
			base(title, clearsScreen, displaysTitle)
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
			base.Display();
			Functionality();
		}
	}
}
