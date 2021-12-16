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

		public static bool Create(Entry entry)
		{
			Debug.Assert(entry != null);

			using (var connection = new SQLiteConnection(_connectionString))
			{
				connection.Open();

				var query = @"INSERT INTO `Entry` (`Name`) VALUES (@Name);";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue("@Name", entry.Name);

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

				var query = @"DELETE FROM `Entry` WHERE `Id` = @Id;";

				using (var command = new SQLiteCommand(query, connection))
				{
					_ = command.Parameters.AddWithValue("@Id", entry.Id);
					return command.ExecuteNonQuery() > 0;
				}
			}
		}
	}
	public class NotUniqueException : Exception {}
}
