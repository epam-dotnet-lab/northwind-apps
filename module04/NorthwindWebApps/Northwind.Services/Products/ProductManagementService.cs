using System;
using System.Collections.Generic;
using System.IO;

namespace Northwind.Services.Products
{
    /// <summary>
    /// Represents a stub for a product management service.
    /// </summary>
    public sealed class ProductManagementService : IProductManagementService
    {
        /// <inheritdoc/>
        public int CreateCategory(ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool DestroyCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool DestroyPicture(int categoryId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool DestroyProduct(int productId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<ProductCategory> LookupCategoriesByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<Product> LookupProductsByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<ProductCategory> ShowCategories(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<Product> ShowProducts(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<Product> ShowProductsForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool TryShowCategory(int categoryId, out ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int productId, out Product product)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool UpdateCategories(int categoryId, ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool UpdatePicture(int categoryId, Stream stream)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool UpdateProduct(int productId, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
