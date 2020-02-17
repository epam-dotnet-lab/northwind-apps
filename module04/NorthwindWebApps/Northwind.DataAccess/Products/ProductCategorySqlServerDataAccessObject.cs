// ReSharper disable CheckNamespace
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Northwind.DataAccess.Products
{
    /// <summary>
    /// Represents a SQL Server-tailored DAO for Northwind product categories.
    /// </summary>
    public sealed class ProductCategorySqlServerDataAccessObject : IProductCategoryDataAccessObject
    {
        private readonly SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategorySqlServerDataAccessObject"/> class.
        /// </summary>
        /// <param name="connection">A <see cref="SqlConnection"/>.</param>
        public ProductCategorySqlServerDataAccessObject(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <inheritdoc/>
        public int InsertProductCategory(ProductCategoryTransferObject productCategory)
        {
            if (productCategory == null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }

            const string commandText =
@"INSERT INTO dbo.Categories (CategoryName, Description, Picture) OUTPUT Inserted.CategoryID
VALUES (@categoryName, @description, @picture)";

            using (var command = new SqlCommand(commandText, this.connection))
            {
                AddSqlParameters(productCategory, command);

                var id = command.ExecuteScalar();
                return (int)id;
            }
        }

        /// <inheritdoc/>
        public bool DeleteProductCategory(int productCategoryId)
        {
            if (productCategoryId <= 0)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(productCategoryId));
            }

            const string commandText =
@"DELETE FROM dbo.Categories WHERE CategoryID = @categoryID
SELECT @@ROWCOUNT";

            using (var command = new SqlCommand(commandText, this.connection))
            {
                const string categoryId = "@categoryID";
                command.Parameters.Add(categoryId, SqlDbType.Int);
                command.Parameters[categoryId].Value = productCategoryId;

                var result = command.ExecuteScalar();
                return ((int)result) > 0;
            }
        }

        /// <inheritdoc/>
        public ProductCategoryTransferObject FindProductCategory(int productCategoryId)
        {
            if (productCategoryId <= 0)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(productCategoryId));
            }

            const string commandText =
@"SELECT c.CategoryID, c.CategoryName, c.Description, c.Picture FROM dbo.Categories as c
WHERE c.CategoryID = @categoryId";

            using (var command = new SqlCommand(commandText, this.connection))
            {
                const string categoryId = "@categoryId";
                command.Parameters.Add(categoryId, SqlDbType.Int);
                command.Parameters[categoryId].Value = productCategoryId;

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new ProductCategoryNotFoundException(productCategoryId);
                    }

                    return CreateProductCategory(reader);
                }
            }
        }

        /// <inheritdoc/>
        public IList<ProductCategoryTransferObject> SelectProductCategories(int offset, int limit)
        {
            if (offset < 0)
            {
                throw new ArgumentException("Must be greater than zero or equals zero.", nameof(offset));
            }

            if (limit < 1)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(limit));
            }

            const string commandTemplate =
@"SELECT c.CategoryID, c.CategoryName, c.Description, c.Picture FROM dbo.Categories as c
ORDER BY c.CategoryID
OFFSET {0} ROWS
FETCH FIRST {1} ROWS ONLY";

            string commandText = string.Format(CultureInfo.CurrentCulture, commandTemplate, offset, limit);
            return this.ExecuteReader(commandText);
        }

        /// <inheritdoc/>
        public IList<ProductCategoryTransferObject> SelectProductCategoriesByName(ICollection<string> productCategoryNames)
        {
            if (productCategoryNames == null)
            {
                throw new ArgumentNullException(nameof(productCategoryNames));
            }

            if (productCategoryNames.Count < 1)
            {
                throw new ArgumentException("Collection is empty.", nameof(productCategoryNames));
            }

            const string commandTemplate =
@"SELECT c.CategoryID, c.CategoryName, c.Description, c.Picture FROM dbo.Categories as c
WHERE c.CategoryName in ('{0}')
ORDER BY c.CategoryID";

            string commandText = string.Format(CultureInfo.CurrentCulture, commandTemplate, string.Join("', '", productCategoryNames));
            return this.ExecuteReader(commandText);
        }

        /// <inheritdoc/>
        public bool UpdateProductCategory(ProductCategoryTransferObject productCategory)
        {
            if (productCategory == null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }

            const string commandText =
@"UPDATE dbo.Categories SET CategoryName = @categoryName, Description = @description, Picture = @picture
WHERE CategoryID = @categoryId
SELECT @@ROWCOUNT";

            using (var command = new SqlCommand(commandText, this.connection))
            {
                AddSqlParameters(productCategory, command);

                const string categoryId = "@categoryId";
                command.Parameters.Add(categoryId, SqlDbType.Int);
                command.Parameters[categoryId].Value = productCategory.Id;

                var result = command.ExecuteScalar();
                return ((int)result) > 0;
            }
        }

        private static ProductCategoryTransferObject CreateProductCategory(SqlDataReader reader)
        {
            var id = (int)reader["CategoryID"];
            var name = (string)reader["CategoryName"];

            const string descriptionColumnName = "Description";
            string description = null;

            if (reader[descriptionColumnName] != DBNull.Value)
            {
                description = (string)reader["Description"];
            }

            const string pictureColumnName = "Picture";
            byte[] picture = null;

            if (reader[pictureColumnName] != DBNull.Value)
            {
                picture = (byte[])reader["Picture"];
            }

            return new ProductCategoryTransferObject
            {
                Id = id,
                Name = name,
                Description = description,
                Picture = picture,
            };
        }

        private static void AddSqlParameters(ProductCategoryTransferObject productCategory, SqlCommand command)
        {
            const string categoryNameParameter = "@categoryName";
            command.Parameters.Add(categoryNameParameter, SqlDbType.NVarChar, 15);
            command.Parameters[categoryNameParameter].Value = productCategory.Name;

            const string descriptionParameter = "@description";
            command.Parameters.Add(descriptionParameter, SqlDbType.NText);
            command.Parameters[descriptionParameter].IsNullable = true;

            if (productCategory.Description != null)
            {
                command.Parameters[descriptionParameter].Value = productCategory.Description;
            }
            else
            {
                command.Parameters[descriptionParameter].Value = DBNull.Value;
            }

            const string pictureParameter = "@picture";
            command.Parameters.Add(pictureParameter, SqlDbType.Image);
            command.Parameters[pictureParameter].IsNullable = true;

            if (productCategory.Picture != null)
            {
                command.Parameters[pictureParameter].Value = productCategory.Picture;
            }
            else
            {
                command.Parameters[pictureParameter].Value = DBNull.Value;
            }
        }

        private IList<ProductCategoryTransferObject> ExecuteReader(string commandText)
        {
            var productCategories = new List<ProductCategoryTransferObject>();

#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
            using (var command = new SqlCommand(commandText, this.connection))
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    productCategories.Add(CreateProductCategory(reader));
                }
            }

            return productCategories;
        }
    }
}
