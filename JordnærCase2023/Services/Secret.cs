using System;

namespace JordnærCase2023.Services
{
    public class Secret
    {
        //Daniel
        private static string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = \"JordnærLocalDB\"; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Frederik
        //private static string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JordnaerCase2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string MyConnectionString
        {
            get { return _connectionString; }
        }
    }
}
