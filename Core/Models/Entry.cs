
using System.Diagnostics;

namespace Core.Models
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
            get => id;

            set
            {
                Debug.Assert(value >= 0);
                id = value;
            }
        }

        public string Name
        {
            get => name;

            set
            {
                value = value.Trim();
                Debug.Assert(!string.IsNullOrEmpty(value));
                name = value;
            }
        }

        public override string ToString() => Name;
    }
}
