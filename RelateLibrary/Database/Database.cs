using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

namespace RelateLibrary.Database
{
	public static class Database
	{
		private static readonly string _connectionString =
				@"Data Source=.\Database.db;Version=3;";

		public static long Create(Entry entry)
		{
			Debug.Assert
			(
				entry != null,
				"The entry cannot be null."
			);

			using (var connection = new SQLiteConnection(_connectionString))
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
			Debug.Assert
			(
				id >= 1,
				"The entry ID must be positive."
			);

			Entry entry = null;

			using (var connection = new SQLiteConnection(_connectionString))
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

		public static List<Entry> ReadEntries()
		{
			var entries = new List<Entry>();

			using (var connection = new SQLiteConnection(_connectionString))
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

				var query = @"SELECT * FROM `Entry`;";

				using (var command = new SQLiteCommand(query, connection))
				{
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

		public static List<Entry> SearchEntry(string search)
		{
			Debug.Assert
			(
				!string.IsNullOrEmpty(search),
				"The search cannot be null or empty."
			);

			search = search.Trim();

			Debug.Assert
			(
				search != "",
				"The search cannot be only whitespace."
			);

			var entries = new List<Entry>();

			using (var connection = new SQLiteConnection(_connectionString))
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
						"SELECT * FROM `Entry`" +
						$"WHERE `Name` LIKE \"%{search}%\"" +
						"COLLATE NOCASE;";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue("@search", search);

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

		public static bool Update(Entry entry)
		{
			Debug.Assert
			(
				entry != null,
				"The entry cannot be null."
			);

			using (var connection = new SQLiteConnection(_connectionString))
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
			Debug.Assert
			(
				entry != null,
				"The entry cannot be null."
			);

			using (var connection = new SQLiteConnection(_connectionString))
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
			Debug.Assert
			(
				relation != null,
				"The relation cannot be null."
			);

			using (var connection = new SQLiteConnection(_connectionString))
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

		public static List<Entry> ReadRelatedEntries(long entryId)
		{
			Debug.Assert
			(
				entryId >= 1,
				"The entry ID must be positive."
			);

			var entries = new List<Entry>();

			using (var connection = new SQLiteConnection(_connectionString))
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
						"SELECT `Id`, `Name` " +
						"FROM `Entry` " +
						"INNER JOIN `Relation` " +
						"ON `Id` = `SecondEntryId` " +
						"WHERE `FirstEntryId` = @entryId " +
						"UNION " +
						"SELECT `Id`, `Name` " +
						"FROM `Entry` " +
						"INNER JOIN `Relation` " +
						"ON `Id` = `FirstEntryId` " +
						"WHERE `SecondEntryId` = @entryId;";

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

		public static List<Entry> ReadRelatableEntries(long entryId)
		{
			Debug.Assert
			(
				entryId >= 1,
				"The entry ID must be positive."
			);

			var entries = new List<Entry>();

			using (var connection = new SQLiteConnection(_connectionString))
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
	}
	public class NotUniqueException : Exception {}
}
