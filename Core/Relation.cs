﻿using System.Diagnostics;

namespace Core
{
	public class Relation
	{
		private Entry firstEntry;
		private Entry secondEntry;

		public Relation(Entry firstEntry, Entry secondEntry)
		{
            FirstEntry = firstEntry;
            SecondEntry = secondEntry;
        }

		public Entry FirstEntry
        {
            get => firstEntry;
            set => firstEntry = Validate(value);
        }

        public Entry SecondEntry
        {
            get => secondEntry;
            set => secondEntry = Validate(value);
        }

        private static Entry Validate(Entry entry)
        {
            Debug.Assert(entry != null);
            Debug.Assert(entry.Id > 0);

            return entry;
        }
    }
}
