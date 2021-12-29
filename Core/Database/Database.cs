using Core.Models;

using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace Core.Database
{
    public static class Database
	{
		private static string ConnectionString { get; } =
				ConfigurationManager
					.ConnectionStrings["SQLite"]
					.ConnectionString;

		public static List<Entry> GetAllEntries()
		{
			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				var query = "PRAGMA foreign_keys = 1;";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.ExecuteNonQuery();
				}

				query = "SELECT `Id`, `Name` FROM `Entry`;";

				using (var command = new SQLiteCommand(query, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						var entries = new List<Entry>();

						while (reader.Read())
						{
							var entry = new Entry(
								reader["Name"].ToString(),
								long.Parse(reader["Id"].ToString())
							);

							entries.Add(entry);
						}

						return entries;
					}
				}
			}
		}
	}
}
