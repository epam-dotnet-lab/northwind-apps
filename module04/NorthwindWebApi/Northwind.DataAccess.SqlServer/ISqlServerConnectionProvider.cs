// ReSharper disable CheckNamespace
using System.Data.SqlClient;

namespace Northwind.DataAccess
{
    /// <summary>
    /// Represents a SQL Server connection provider.
    /// </summary>
    public interface ISqlServerConnectionProvider
    {
        /// <summary>
        /// Gets a connection to SQL Server.
        /// </summary>
        /// <returns>A <see cref="SqlConnection"/>.</returns>
        SqlConnection GetConnection();
    }
}
