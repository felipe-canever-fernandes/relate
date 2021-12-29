using System.Diagnostics;

namespace Core
{
	public class Entry
    {
        private long id;
        private string name;

        public Entry(string name, long id = 0)
        {
            Id = id;
            Name = name;
        }

        public long Id
        {
            get { return id; }

            set
            {
                Debug.Assert
                (
                    value >= 0,
                    "The entry ID cannot be negative."
                );

                id = value;
            }
        }

        public string Name
        {
            get { return name; }

            set
            {
                Debug.Assert
                (
                    !string.IsNullOrEmpty(value),
                    "The entry name cannot be null or empty."
                );

                value = value.Trim();

                Debug.Assert
                (
                    value != "",
                    "The entry name cannot be only whitespace."
                );

                name = value;
            }
        }

		public override string ToString()
		{
            return $"[{Name}]";
        }
	}
}
