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
		private delegate void CommandCallback(SQLiteCommand command);

		private static string ConnectionString { get; } =
				ConfigurationManager
					.ConnectionStrings["SQLite"]
					.ConnectionString;

		public static Entry Insert(Entry entry)
		{
			Debug.Assert(!(entry is null));

			var query = "INSERT INTO `Entry` (`Name`) VALUES (@Name);";

			ExecuteCommand(query, CommandCallback);

			return entry;

			void CommandCallback(SQLiteCommand command)
			{
				_ = command.Parameters.AddWithValue("@Name", entry.Name);
				_ = command.ExecuteNonQuery();

				entry.Id = command.Connection.LastInsertRowId;
			}
		}

		public static bool Exists(Entry entry)
		{
			Debug.Assert(!(entry is null));

			var query =
				"SELECT EXISTS(SELECT 1 FROM `Entry` WHERE `Name` = @Name);";

			var exists = false;

			ExecuteCommand(query.ToString(), CommandCallback);

			return exists;

			void CommandCallback(SQLiteCommand command)
			{
				_ = command.Parameters.AddWithValue("@Name", entry.Name);

				using (var reader = command.ExecuteReader())
				{
					_ = reader.Read();
					exists = reader.GetBoolean(0);
				}
			}
		}

		public static List<Entry> GetEntries(string search = "")
		{
			Debug.Assert(!(search is null));

			var query = BuildQuery();
			var entries = new List<Entry>();

			ExecuteCommand(query.ToString(), CommandCallback);

			return entries;

			string BuildQuery()
			{
				var queryString =
					new StringBuilder("SELECT `Id`, `Name` FROM `Entry`");

				if (search != string.Empty)
				{
					_ = queryString.Append(
						$" WHERE `Name` LIKE \"%{search}%\" COLLATE NOCASE"
					);
				}

				_ = queryString.Append(" ORDER BY `Name` ASC;");

				return queryString.ToString();
			}

			void CommandCallback(SQLiteCommand command)
			{
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var entry = new Entry(
							reader["Name"].ToString(),
							long.Parse(reader["Id"].ToString())
						);

						entries.Add(entry);
					}
				}
			}
		}

		private static void ExecuteCommand(
			string query,
			CommandCallback commandCallback
		)
		{
			Debug.Assert(!string.IsNullOrEmpty(query));

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				EnforceForeignKeyConstraints(connection);

				using (var command = new SQLiteCommand(query, connection))
				{
					commandCallback(command);
				}
			}

			void EnforceForeignKeyConstraints(SQLiteConnection connection)
			{
				connection.Open();

				using (
					var command =
						new SQLiteCommand(
							"PRAGMA foreign_keys = 1;",
							connection
						)
				)
				{
					_ = command.ExecuteNonQuery();
				}
			}
		}
	}
}
