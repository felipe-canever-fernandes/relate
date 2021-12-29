using System.Diagnostics;

namespace Core
{
	public class Relation
	{
		private long firstEntryId;
		private long secondEntryId;

		public Relation(long firstEntryId, long secondEntryId)
		{
            FirstEntryId = firstEntryId;
            SecondEntryId = secondEntryId;
		}

        public long FirstEntryId
        {
            get => firstEntryId;

            set
            {
                Debug.Assert(value > 0);
                firstEntryId = value;
            }
        }

        public long SecondEntryId
        {
            get => secondEntryId;

            set
            {
                Debug.Assert(value > 0);
                secondEntryId = value;
            }
        }
    }
}
