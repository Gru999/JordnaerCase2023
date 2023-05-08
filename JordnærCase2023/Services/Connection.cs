using Microsoft.AspNetCore.DataProtection;

namespace JordnærCase2023.Services
{
    public abstract class Connection
    {
        protected string connectionString;
        public IConfiguration Configuration { get; set; }
        public Connection(IConfiguration configuration)
        {
            connectionString = Secret.MyProperty;
            Configuration = configuration;
        }
        public Connection(string connectionString)
        {
            Configuration = null;
            this.connectionString = connectionString;
        }
    }

    }
}
