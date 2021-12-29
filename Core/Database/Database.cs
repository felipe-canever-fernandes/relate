using System.Configuration;

namespace Core.Database
{
    public static class Database
	{
		private static string ConnectionString { get; } =
				ConfigurationManager
					.ConnectionStrings["SQLite"]
					.ConnectionString;
	}
}
