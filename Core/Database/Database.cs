using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Diagnostics;

namespace Core.Database
{
	public static class Database
	{
		private static string ConnectionString { get; } =
				ConfigurationManager
					.ConnectionStrings["SQLite"]
					.ConnectionString;

		public static long Create(Entry entry)
		{
			Debug.Assert(entry != null);

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				using
				(
					var command = new SQLiteCommand
					(
						"PRAGMA foreign_keys = 1;",
						connection
					)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				var query = @"INSERT INTO `Entry` (`Name`) VALUES (@Name);";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue("@Name", entry.Name);

					try
					{
						if (command.ExecuteNonQuery() <= 0)
						{
							return 0;
						}
					}
					catch (SQLiteException ex)
					{
						if (ex.Message.Contains("UNIQUE"))
							throw new NotUniqueException();

						throw;
					}

					return connection.LastInsertRowId;
				}
			}
		}

		public static Entry ReadEntry(long id)
		{
			Debug.Assert(id >= 1);

			Entry entry = null;

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				using
				(
					var command = new SQLiteCommand
					(
						"PRAGMA foreign_keys = 1;",
						connection
					)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				var query = @"SELECT * FROM `Entry` WHERE `Id` = @Id;";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue("@Id", id);

					using (var reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							_ = reader.Read();

							entry = new Entry
							(
								reader["Name"].ToString(),
								long.Parse(reader["Id"].ToString())
							);
						}
					}
				}
			}

			return entry;
		}

		public static long GetEntryId(string entryName)
		{
			Debug.Assert(entryName != null);

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				using
				(
					var command = new SQLiteCommand
					(
						"PRAGMA foreign_keys = 1;",
						connection
					)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				var query = "SELECT `Id` FROM `Entry` WHERE `Name` = @Name;";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue("@Name", entryName);

					using (var reader = command.ExecuteReader())
					{
						if (!reader.HasRows)
							return 0;

						_ = reader.Read();
						return long.Parse(reader["Id"].ToString());
					}
				}
			}
		}

		public static List<Entry> ReadEntries
		(
			Entry related = null,
			string search = ""
		)
		{
			Debug.Assert(search != null);

			search = search.Trim();

			var entries = new List<Entry>();

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				using
				(
					var command = new SQLiteCommand
					(
						"PRAGMA foreign_keys = 1;",
						connection
					)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				var query = @"SELECT * FROM";

				if (related == null)
				{
					query += " `Entry`";
				}
				else
				{
					query +=
						" (" +
						"SELECT `Id`, `Name` " +
						"FROM `Entry` " +
						"INNER JOIN `Relation` " +
						"ON `Id` = `SecondEntryId` " +
						"WHERE `FirstEntryId` = @relatedId " +
						"UNION " +
						"SELECT `Id`, `Name` " +
						"FROM `Entry` " +
						"INNER JOIN `Relation` " +
						"ON `Id` = `FirstEntryId` " +
						"WHERE `SecondEntryId` = @relatedId" +
						")";
				}

				if (!string.IsNullOrEmpty(search))
				{
					query +=
						$" WHERE `Name` LIKE \"%{search}%\"" +
						"COLLATE NOCASE";
				}

				query += " ORDER BY `Name`;";

				using (var command = new SQLiteCommand(query, connection))
				{
					if (related != null)
					{
						_ = command.Parameters.AddWithValue
						(
							"@relatedId",
							related.Id
						);
					}

					using (var reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								var entry = new Entry
								(
									reader["Name"].ToString(),
									long.Parse(reader["Id"].ToString())
								);

								entries.Add(entry);
							}
						}
					}
				}
			}

			return entries;
		}

		public static bool AreRelated(Entry firstEntry, Entry secondEntry)
		{
			Debug.Assert(firstEntry != null);
			Debug.Assert(secondEntry != null);

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				using
				(
					var command = new SQLiteCommand
					(
						"PRAGMA foreign_keys = 1;",
						connection
					)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				var query =
						"SELECT * " +
						"FROM `Relation` " +
						"WHERE " +
						"	`FirstEntryId` = @firstEntryId AND " +
						"		`SecondEntryId` = @secondEntryId " +
						"	OR " +
						"	`SecondEntryId` = @firstEntryId AND " +
						"		`FirstEntryId` = @secondEntryId;";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue
					(
						"@firstEntryId",
						firstEntry.Id
					);

					_ = command.Parameters.AddWithValue
					(
						"@secondEntryId",
						secondEntry.Id
					);

					using (var reader = command.ExecuteReader())
					{
						return reader.HasRows;
					}
				}
			}
		}

		public static bool Update(Entry entry)
		{
			Debug.Assert(entry != null);

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				using
				(
					var command = new SQLiteCommand
					(
						"PRAGMA foreign_keys = 1;",
						connection
					)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				var query =
					@"UPDATE `Entry`
					SET `Name` = @Name
					WHERE `Id` = @Id;";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue("@Name", entry.Name);
					_ = command.Parameters.AddWithValue("@Id", entry.Id);

					try
					{
						return command.ExecuteNonQuery() > 0;
					}
					catch (SQLiteException ex)
					{
						if (ex.Message.Contains("UNIQUE"))
							throw new NotUniqueException();

						throw;
					}
				}
			}
		}

		public static bool Delete(Entry entry)
		{
			Debug.Assert(entry != null);

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				using
				(
					var command = new SQLiteCommand
					(
						"PRAGMA foreign_keys = 1;",
						connection
					)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				var query = @"DELETE FROM `Entry` WHERE `Id` = @Id;";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue("@Id", entry.Id);
					return command.ExecuteNonQuery() > 0;
				}
			}
		}

		public static bool Create(Relation relation)
		{
			Debug.Assert(relation != null);

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				using
				(
					var command = new SQLiteCommand
					(
						"PRAGMA foreign_keys = 1;",
						connection
					)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				var query =
						"INSERT INTO " +
							"`Relation` (`FirstEntryId`, `SecondEntryId`)" +
						"VALUES (@FirstEntryId, @SecondEntryId);";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue
					(
						"@FirstEntryId",
						relation.FirstEntryId
					);

					_ = command.Parameters.AddWithValue
					(
						"@SecondEntryId",
						relation.SecondEntryId
					);

					try
					{
						return command.ExecuteNonQuery() > 0;
					}
					catch (SQLiteException ex)
					{
						if (ex.Message.Contains("UNIQUE"))
							throw new NotUniqueException();

						throw;
					}
				}
			}
		}

		public static List<Entry> ReadRelatableEntries(long entryId)
		{
			Debug.Assert(entryId >= 1);

			var entries = new List<Entry>();

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				using
				(
					var command = new SQLiteCommand
					(
						"PRAGMA foreign_keys = 1;",
						connection
					)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				var query =
						"SELECT * " +
						"FROM `Entry` " +
						"WHERE `Id` != @entryId " +
						"EXCEPT " +
						"SELECT * " +
						"FROM " +
						"( " +
						"	SELECT `Id`, `Name` " +
						"	FROM `Entry` " +
						"	INNER JOIN `Relation` " +
						"	ON `Id` = `SecondEntryId` " +
						"	WHERE `FirstEntryId` = @entryId " +
						"	UNION " +
						"	SELECT `Id`, `Name` " +
						"	FROM `Entry` " +
						"	INNER JOIN `Relation` " +
						"	ON `Id` = `FirstEntryId` " +
						"	WHERE `SecondEntryId` = @entryId " +
						");";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue
					(
						"@entryId",
						entryId
					);

					using (var reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								var entry = new Entry
								(
									reader["Name"].ToString(),
									long.Parse(reader["Id"].ToString())
								);

								entries.Add(entry);
							}
						}
					}
				}
			}

			return entries;
		}

		public static bool Delete(Relation relation)
		{
			Debug.Assert(relation != null);

			using (var connection = new SQLiteConnection(ConnectionString))
			{
				connection.Open();

				using
				(
					var command = new SQLiteCommand
					(
						"PRAGMA foreign_keys = 1;",
						connection
					)
				)
				{
					_ = command.ExecuteNonQuery();
				}

				var query =
						"DELETE FROM `Relation` " +
						"WHERE " +
						"	(`FirstEntryId` = @FirstEntryId " +
						"	AND `SecondEntryId` = @SecondEntryId) " +
						"	OR " +
						"	(`SecondEntryId` = @FirstEntryId " +
						"	AND `FirstEntryId` = @SecondEntryId);";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue
					(
						"@FirstEntryId",
						relation.FirstEntryId
					);

					_ = command.Parameters.AddWithValue
					(
						"@SecondEntryId",
						relation.SecondEntryId
					);

					return command.ExecuteNonQuery() > 0;
				}
			}
		}
	}
	public class NotUniqueException : Exception {}
}
