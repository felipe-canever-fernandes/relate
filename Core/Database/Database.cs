using Core.Models;

using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;

namespace Core.Database
{
	public static class Database
	{
		private static string ConnectionString { get; } =
				ConfigurationManager
					.ConnectionStrings["SQLite"]
					.ConnectionString;

		public static List<Entry> GetEntries(string filter = "")
		{
			Debug.Assert(filter != null);

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				var query = new StringBuilder("PRAGMA foreign_keys = 1;");

				using (
					var command =
						new SQLiteCommand(query.ToString(), connection)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				query = new StringBuilder("SELECT `Id`, `Name` FROM `Entry`");

				if (filter != string.Empty)
				{
					_ = query.Append(
						$" WHERE `Name` LIKE \"%{filter}%\" COLLATE NOCASE"
					);
				}

				_ = query.Append(";");

				using (
					var command =
						new SQLiteCommand(query.ToString(), connection)
				)
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
