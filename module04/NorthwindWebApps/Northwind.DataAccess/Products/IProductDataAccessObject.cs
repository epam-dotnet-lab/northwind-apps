using System.Collections.Generic;

namespace Northwind.DataAccess.Products
{
    /// <summary>
    /// Represents a DAO for Northwind products.
    /// </summary>
    public interface IProductDataAccessObject
    {
        /// <summary>
        /// Inserts a new Northwind product to a data storage.
        /// </summary>
        /// <param name="product">A <see cref="ProductTransferObject"/>.</param>
        /// <returns>A data storage identifier of a new product.</returns>
        int InsertProduct(ProductTransferObject product);

        /// <summary>
        /// Deletes a Northwind product from a data storage.
        /// </summary>
        /// <param name="productId">An product identifier.</param>
        /// <returns>True if a product is deleted; otherwise false.</returns>
        bool DeleteProduct(int productId);

        /// <summary>
        /// Updates a Northwind product in a data storage.
        /// </summary>
        /// <param name="product">A <see cref="ProductTransferObject"/>.</param>
        /// <returns>True if a product is updated; otherwise false.</returns>
        bool UpdateProduct(ProductTransferObject product);

        /// <summary>
        /// Finds a Northwind product using a specified identifier.
        /// </summary>
        /// <param name="productId">A data storage identifier of an existed product.</param>
        /// <returns>A <see cref="ProductTransferObject"/> with specified identifier.</returns>
        ProductTransferObject FindProduct(int productId);

        /// <summary>
        /// Selects products using specified offset and limit.
        /// </summary>
        /// <param name="offset">An offset of the first object.</param>
        /// <param name="limit">A limit of returned objects.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="ProductTransferObject"/>.</returns>
        IList<ProductTransferObject> SelectProducts(int offset, int limit);

        /// <summary>
        /// Selects all Northwind products with specified names.
        /// </summary>
        /// <param name="productNames">A <see cref="IEnumerable{T}"/> of product names.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="ProductTransferObject"/>.</returns>
        IList<ProductTransferObject> SelectProductsByName(ICollection<string> productNames);

        /// <summary>
        /// Selects all Northwind products that belongs to specified categories.
        /// </summary>
        /// <param name="collectionOfCategoryId">A <see cref="ICollection{T}"/> of category id.</param>
        /// <returns>A <see cref="IList{T}"/> of <see cref="ProductTransferObject"/>.</returns>
        IList<ProductTransferObject> SelectProductByCategory(ICollection<int> collectionOfCategoryId);
    }
}
