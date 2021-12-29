using System.Configuration;

namespace Core.Database
{
    public static class Database
	{
		public static string ConnectionString { get; } =
				ConfigurationManager
					.ConnectionStrings["SQLite"]
					.ConnectionString;
	}
}
