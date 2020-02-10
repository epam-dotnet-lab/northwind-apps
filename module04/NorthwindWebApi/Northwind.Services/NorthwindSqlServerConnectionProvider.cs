// ReSharper disable CheckNamespace
using System.Data.SqlClient;
using Northwind.DataAccess;

namespace Northwind.Services
{
    /// <summary>
    /// Represents a SQL Server connection provider for Northwind database.
    /// </summary>
    public class NorthwindSqlServerConnectionProvider : ISqlServerConnectionProvider
    {
        /// <inheritdoc/>
        public SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
