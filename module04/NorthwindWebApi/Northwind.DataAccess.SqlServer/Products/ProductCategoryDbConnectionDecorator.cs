// ReSharper disable CheckNamespace
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Northwind.DataAccess.Products
{
    /// <summary>
    /// Represents a decorator for product category DAO to manage a database connection.
    /// </summary>
    public sealed class ProductCategoryDbConnectionDecorator : IProductCategoryDataAccessObject
    {
        private readonly SqlConnection connection;
        private readonly IProductCategoryDataAccessObject productCategoryDao;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryDbConnectionDecorator"/> class.
        /// </summary>
        /// <param name="connection">A <see cref="SqlConnection"/>.</param>
        /// <param name="productCategoryDao">A <see cref="IProductCategoryDataAccessObject"/>.</param>
        public ProductCategoryDbConnectionDecorator(SqlConnection connection, IProductCategoryDataAccessObject productCategoryDao)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.productCategoryDao = productCategoryDao ?? throw new ArgumentNullException(nameof(productCategoryDao));
        }

        /// <inheritdoc/>
        public int InsertProductCategory(ProductCategoryTransferObject productCategory)
        {
            this.connection.Open();
            try
            {
                return this.productCategoryDao.InsertProductCategory(productCategory);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc/>
        public bool DeleteProductCategory(int productCategoryId)
        {
            this.connection.Open();
            try
            {
                return this.productCategoryDao.DeleteProductCategory(productCategoryId);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc/>
        public ProductCategoryTransferObject FindProductCategory(int productCategoryId)
        {
            this.connection.Open();
            try
            {
                return this.productCategoryDao.FindProductCategory(productCategoryId);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc/>
        public IList<ProductCategoryTransferObject> SelectProductCategories(int offset, int limit)
        {
            this.connection.Open();
            try
            {
                return this.productCategoryDao.SelectProductCategories(offset, limit);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc/>
        public IList<ProductCategoryTransferObject> SelectProductCategoriesByName(ICollection<string> productCategoryNames)
        {
            this.connection.Open();
            try
            {
                return this.productCategoryDao.SelectProductCategoriesByName(productCategoryNames);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <inheritdoc/>
        public bool UpdateProductCategory(ProductCategoryTransferObject productCategory)
        {
            this.connection.Open();
            try
            {
                return this.productCategoryDao.UpdateProductCategory(productCategory);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
