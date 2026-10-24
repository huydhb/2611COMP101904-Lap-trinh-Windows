using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace Lab12.DAL
{
    public static class Database
    {
        private static readonly string DefaultConnectionString =
            "Server=localhost;Database=QLSinhVienLab12;Integrated Security=True;TrustServerCertificate=True;";

        public static string ConnectionString
        {
            get
            {
                var setting = ConfigurationManager.ConnectionStrings["QLSinhVienLab12"];
                if (setting != null && !string.IsNullOrWhiteSpace(setting.ConnectionString))
                {
                    return setting.ConnectionString;
                }
                return DefaultConnectionString;
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
