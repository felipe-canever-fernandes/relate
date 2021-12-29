using System.Diagnostics;

namespace Core
{
	public class Relation
	{
		private long _firstEntryId;
		private long _secondEntryId;

		public Relation(long firstEntryId, long secondEntryId)
		{
            FirstEntryId = firstEntryId;
            SecondEntryId = secondEntryId;
		}

        public long FirstEntryId
        {
            get { return _firstEntryId; }

            set
            {
                Debug.Assert
                (
                    value > 0,
                    "The first entry ID must be positive."
                );

                _firstEntryId = value;
            }
        }

        public long SecondEntryId
        {
            get { return _secondEntryId; }

            set
            {
                Debug.Assert
                (
                    value > 0,
                    "The second entry ID must be positive."
                );

                _secondEntryId = value;
            }
        }
    }
}
