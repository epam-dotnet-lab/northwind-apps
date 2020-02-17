using System.Diagnostics;

namespace Northwind.DataAccess.Products
{
    /// <summary>
    /// Represents a TO for Northwind product categories.
    /// </summary>
    [DebuggerDisplay("Id={Id}, Name={Name}")]
    public sealed class ProductCategoryTransferObject
    {
        /// <summary>
        /// Gets or sets a product category identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a product category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a product category description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a product category picture.
        /// </summary>
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] Picture { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
