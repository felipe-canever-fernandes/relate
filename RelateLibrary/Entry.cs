using System;

namespace RelateLibrary
{
    public class Entry
    {
        private int _id;
        private string _name;

        public int Id
        {
            get { return _id; }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException
                    (
                        nameof(value),
                        value,
                        "the entry ID cannot be negative"
                    );

                _id = value;
            }
        }

        public string Name
        {
            get { return _name; }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException
                    (
                        "the entry name cannot be empty",
                        nameof(value)
                    );

                if (value.Trim() == "")
                    throw new ArgumentException
                    (
                        "the entry name cannot be only whitespace",
                        nameof(value)
                    );

                _name = value.Trim();
            }
        }

        public Entry(string name, int id = 0)
        {
            Id = id;
            Name = name;
        }
    }
}
