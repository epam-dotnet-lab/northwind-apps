using System.Collections.Generic;

namespace Northwind.DataAccess.Products
{
    /// <summary>
    /// Represents a DAO for Northwind product categories.
    /// </summary>
    public interface IProductCategoryDataAccessObject
    {
        /// <summary>
        /// Inserts a new Northwind product category to a data storage.
        /// </summary>
        /// <param name="productCategory">A <see cref="ProductCategoryTransferObject"/>.</param>
        /// <returns>A data storage identifier of a new product category.</returns>
        int InsertProductCategory(ProductCategoryTransferObject productCategory);

        /// <summary>
        /// Deletes a Northwind product category from a data storage.
        /// </summary>
        /// <param name="productCategoryId">An product category identifier.</param>
        /// <returns>True if a product category is deleted; otherwise false.</returns>
        bool DeleteProductCategory(int productCategoryId);

        /// <summary>
        /// Updates a Northwind product category in a data storage.
        /// </summary>
        /// <param name="productCategory">A <see cref="ProductCategoryTransferObject"/>.</param>
        /// <returns>True if a product category is updated; otherwise false.</returns>
        bool UpdateProductCategory(ProductCategoryTransferObject productCategory);

        /// <summary>
        /// Finds a Northwind product category using a specified identifier.
        /// </summary>
        /// <param name="productCategoryId">A data storage identifier of an existed product category.</param>
        /// <returns>A <see cref="ProductCategoryTransferObject"/> with specified identifier.</returns>
        ProductCategoryTransferObject FindProductCategory(int productCategoryId);

        /// <summary>
        /// Selects product categories using specified offset and limit.
        /// </summary>
        /// <param name="offset">An offset of the first object.</param>
        /// <param name="limit">A limit of returned objects.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="ProductCategoryTransferObject"/>.</returns>
        IList<ProductCategoryTransferObject> SelectProductCategories(int offset, int limit);

        /// <summary>
        /// Selects all Northwind product categories with specified names.
        /// </summary>
        /// <param name="productCategoryNames">A <see cref="ICollection{T}"/> of product category names.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="ProductCategoryTransferObject"/>.</returns>
        IList<ProductCategoryTransferObject> SelectProductCategoriesByName(ICollection<string> productCategoryNames);
    }
}
