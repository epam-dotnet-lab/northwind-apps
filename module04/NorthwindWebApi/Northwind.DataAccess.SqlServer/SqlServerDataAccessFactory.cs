// ReSharper disable CheckNamespace
using System;
using Northwind.DataAccess.LegacySqlServer.Products;
using Northwind.DataAccess.Products;

namespace Northwind.DataAccess
{
    /// <summary>
    /// Represents an abstract factory for creating Northwind DAO for SQL Server.
    /// </summary>
    public sealed class SqlServerDataAccessFactory : NorthwindDataAccessFactory
    {
        private readonly ISqlServerConnectionProvider connectionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDataAccessFactory"/> class.
        /// </summary>
        /// <param name="connectionProvider">A database connection to SQL Server.</param>
        public SqlServerDataAccessFactory(ISqlServerConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
        }

        /// <inheritdoc/>
        public override IProductCategoryDataAccessObject GetProductCategoryDataAccessObject()
        {
            var connection = this.connectionProvider.GetConnection();
            var dao = new ProductCategorySqlServerDataAccessObject(connection);
            return new ProductCategoryDbConnectionDecorator(connection, dao);
        }

        /// <inheritdoc/>
        public override IProductDataAccessObject GetProductDataAccessObject()
        {
            var connection = this.connectionProvider.GetConnection();
            var dao = new ProductSqlServerDataAccessObject(connection);
            return new ProductDbConnectionDecorator(connection, dao);
        }
    }
}
