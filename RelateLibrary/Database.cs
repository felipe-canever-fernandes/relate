using System;
using System.Data.SQLite;

namespace RelateLibrary
{
    public static class Database
    {
        private static readonly string _connectionString =
                @"Data Source=.\Database.db;Version=3;";

        public static void Create(Entry entry)
        {
            if (entry == null)
                throw new ArgumentException
                (
                    "the entry to be inserted cannot be null",
                    nameof(entry)
                );

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                var query = @"INSERT INTO `Entry` (`Name`) VALUES (@Name);";

                using (var command = new SQLiteCommand(query, connection))
                {
                    _ = command.Parameters.AddWithValue("@Name", entry.Name);
                    _ = command.ExecuteNonQuery();
                }
            }
        }
    }
}
