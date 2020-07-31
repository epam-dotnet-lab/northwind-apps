using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.ReportingServices.OData.ProductReports
{
    /// <summary>
    /// Represents a report with product lines.
    /// </summary>
    /// <typeparam name="T">Product report line.</typeparam>
    public class ProductReport<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductReport{T}"/> class.
        /// </summary>
        public ProductReport()
        {
            this.Products = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductReport{T}"/> class with specified products.
        /// </summary>
        /// <param name="products">A list of lines of type <typeparamref name="T"/>.</param>
        public ProductReport(IEnumerable<T> products)
        {
            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            this.Products = products.ToArray();
        }

        /// <summary>
        /// Gets a product list.
        /// </summary>
        public IList<T> Products { get; }
    }
}
