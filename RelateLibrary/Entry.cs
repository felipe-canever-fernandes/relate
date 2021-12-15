using System.Diagnostics;

namespace RelateLibrary
{
	public class Entry
    {
        private int _id;
        private string _name;

        public Entry(string name, int id = 0)
        {
            Id = id;
            Name = name;
        }

        public int Id
        {
            get { return _id; }

            set
            {
                Debug.Assert
                (
                    value >= 0,
                    "The entry ID cannot be negative."
                );

                _id = value;
            }
        }

        public string Name
        {
            get { return _name; }

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

                _name = value;
            }
        }
    }
}
