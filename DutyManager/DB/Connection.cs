using System.Data.SqlClient;

namespace DutyManager.DB
{
    public class Connection
    {
        public static Connection Instance { get; set; } = new Connection();
        public string Server { get; set; } = "DIMKAPC\\SQLEXPRESS";
        public string Database { get; set; } = "DB";

        public SqlConnection GetNewConnection() => new SqlConnection(new SqlConnectionStringBuilder()
        {
            DataSource = Server,
            IntegratedSecurity = true,
            InitialCatalog = Database
        }.ConnectionString);
        public string ConnectionString() => new SqlConnectionStringBuilder()
        {
            DataSource = Server,
            IntegratedSecurity = true,
            InitialCatalog = Database
        }.ConnectionString;
        private Connection() { }
    }
}
