﻿using System;

namespace JordnærCase2023.Services
{
    public class Secret
    {
        private static string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = \"JordnærLocalDB\"; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string MyProperty
        {
            get { return _connectionString; }
        }
    }
}
