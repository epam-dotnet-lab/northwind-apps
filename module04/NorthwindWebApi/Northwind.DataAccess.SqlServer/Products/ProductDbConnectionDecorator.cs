// ReSharper disable CheckNamespace
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Northwind.DataAccess.Products;

namespace Northwind.DataAccess.LegacySqlServer.Products
{
    /// <summary>
    /// Represents a decorator for product DAO to manage a database connection.
    /// </summary>
    public sealed class ProductDbConnectionDecorator : IProductDataAccessObject
    {
        private readonly SqlConnection connection;
        private readonly IProductDataAccessObject productDao;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDbConnectionDecorator"/> class.
        /// </summary>
        /// <param name="connection">A <see cref="SqlConnection"/>.</param>
        /// <param name="productDao">A <see cref="IProductDataAccessObject"/>.</param>
        public ProductDbConnectionDecorator(SqlConnection connection, IProductDataAccessObject productDao)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.productDao = productDao ?? throw new ArgumentNullException(nameof(productDao));
        }

        /// <inheritdoc />
        public int InsertProduct(ProductTransferObject product)
        {
            this.connection.Open();
            try
            {
                return this.productDao.InsertProduct(product);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc />
        public bool DeleteProduct(int productId)
        {
            this.connection.Open();
            try
            {
                return this.productDao.DeleteProduct(productId);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc />
        public ProductTransferObject FindProduct(int productId)
        {
            this.connection.Open();
            try
            {
                return this.productDao.FindProduct(productId);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc />
        public IList<ProductTransferObject> SelectProducts(int offset, int limit)
        {
            this.connection.Open();
            try
            {
                return this.productDao.SelectProducts(offset, limit);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc />
        public IList<ProductTransferObject> SelectProductsByName(ICollection<string> productNames)
        {
            this.connection.Open();
            try
            {
                return this.productDao.SelectProductsByName(productNames);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc />
        public IList<ProductTransferObject> SelectProductByCategory(ICollection<int> collectionOfCategoryId)
        {
            this.connection.Open();
            try
            {
                return this.productDao.SelectProductByCategory(collectionOfCategoryId);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc />
        public bool UpdateProduct(ProductTransferObject product)
        {
            this.connection.Open();
            try
            {
                return this.productDao.UpdateProduct(product);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
