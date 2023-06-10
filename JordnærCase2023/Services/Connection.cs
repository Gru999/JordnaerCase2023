using Microsoft.AspNetCore.DataProtection;

namespace JordnærCase2023.Services
{
    public abstract class Connection
    {
        protected string connectionString;
        public IConfiguration Configuration { get; set; }

        public Connection()
        {
            connectionString = Secret.MyConnectionString;
        }
        public Connection(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Secret.MyConnectionString; /*Configuration["ConnectionStrings:DefaultConnection"];*/
        }
        public Connection(string connectionString)
        {
            Configuration = null;
            this.connectionString = connectionString;
        }
    }
}
