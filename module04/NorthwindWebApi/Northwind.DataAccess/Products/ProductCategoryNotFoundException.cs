using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Northwind.DataAccess.Products
{
    /// <summary>
    /// The exception that is thrown when a product category is not found in a data storage.
    /// </summary>
    [Serializable]
#pragma warning disable CA1032 // Implement standard exception constructors
    public class ProductCategoryNotFoundException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryNotFoundException"/> class with specified identifier and object type.
        /// </summary>
        /// <param name="id">A requested identifier.</param>
        public ProductCategoryNotFoundException(int id)
            : base(string.Format(CultureInfo.InvariantCulture, $"A product category with identifier = {id}."))
        {
            this.ProductCategoryId = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">An object that describes the source or destination of the serialized data.</param>
        protected ProductCategoryNotFoundException(SerializationInfo info, StreamingContext context)
              : base(info, context)
        {
        }

        /// <summary>
        /// Gets an identifier of a product category that is missed in a data storage.
        /// </summary>
        public int ProductCategoryId { get; }
    }
}
