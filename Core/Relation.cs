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
            get { return firstEntryId; }

            set
            {
                Debug.Assert
                (
                    value > 0,
                    "The first entry ID must be positive."
                );

                firstEntryId = value;
            }
        }

        public long SecondEntryId
        {
            get { return secondEntryId; }

            set
            {
                Debug.Assert
                (
                    value > 0,
                    "The second entry ID must be positive."
                );

                secondEntryId = value;
            }
        }
    }
}
