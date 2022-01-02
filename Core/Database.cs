using Core.Models;

using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;

namespace Core
{
	public static class Database
	{
		private delegate void CommandCallback(SQLiteCommand command);

		public enum FilterType
		{
			All,
			Related,
			Unrelated
		}

		private static string ConnectionString { get; } =
				ConfigurationManager
					.ConnectionStrings["SQLite"]
					.ConnectionString;

		public static void Insert(Entry entry)
		{
			Debug.Assert(!(entry is null));

			var query = "INSERT INTO `Entry` (`Name`) VALUES (@Name);";
			ExecuteCommand(query, CommandCallback);

			void CommandCallback(SQLiteCommand command)
			{
				_ = command.Parameters.AddWithValue("@Name", entry.Name);
				_ = command.ExecuteNonQuery();
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

		public static List<Entry> GetEntries(
			string search,
			FilterType filterType,
			Entry reference
		)
		{
			Debug.Assert(!(search is null));

			if (reference is null)
			{
				Debug.Assert(filterType == FilterType.All);
			}
			else
			{
				Debug.Assert(reference.Id > 0);
			}

			var entries = new List<Entry>();
			ExecuteCommand(GetQuery(), CommandCallback);
			return entries;

			string GetQuery()
			{
				var query = string.Empty;

				if (filterType == FilterType.All)
				{
					query +=
						"SELECT `Id`, `Name` FROM `Entry` " +
						$"WHERE `Name` LIKE \"%{search}%\" COLLATE NOCASE ";
				}
				else
				{
					if (filterType == FilterType.Unrelated)
					{
						query +=
							"SELECT `Id`, `Name` " +
							"FROM `Entry` " +
							"WHERE `Id` != @Id " +
							"EXCEPT ";
					}


					query +=
						"SELECT * FROM ( " +
						"SELECT `Id`, `Name` " +
						"FROM `Relation` " +
						"INNER JOIN `Entry` " +
						"ON `SecondEntryId` = `Id` " +
						$"WHERE `FirstEntryId` = @Id AND `Name` LIKE \"%{search}%\" " +
						"UNION " +
						"SELECT `Id`, `Name` " +
						"FROM `Relation` " +
						"INNER JOIN `Entry` " +
						"ON `FirstEntryId` = `Id` " +
						$"WHERE `SecondEntryId` = @Id AND `Name` LIKE \"%{search}%\" " +
						"COLLATE NOCASE " +
						")";
				}

				query += "ORDER BY `Name` ASC;";

				return query;
			}

			void CommandCallback(SQLiteCommand command)
			{
				if (!(reference is null))
				{
					_ = command.Parameters.AddWithValue("@Id", reference.Id);
				}

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

		public static void Delete(Entry entry)
		{
			Debug.Assert(!(entry is null));
			Debug.Assert(entry.Id > 0);

			var query = "DELETE FROM `Entry` WHERE `Id` = @Id;";
			ExecuteCommand(query, CommandCallback);

			void CommandCallback(SQLiteCommand command)
			{
				_ = command.Parameters.AddWithValue("@Id", entry.Id);
				_ = command.ExecuteNonQuery();
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
