using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Threading.Tasks;
using Northwind.CurrencyServices.CountryCurrency;
using Northwind.CurrencyServices.CurrencyExchange;
using Northwind.ReportingService.ProductReports;
using NorthwindProduct = NorthwindModel.Product;

namespace Northwind.ReportingServices.OData.ProductReports
{
    /// <summary>
    /// Represents a service that produces product-related reports.
    /// </summary>
    public class ProductReportService : IProductReportService
    {
        private readonly NorthwindModel.NorthwindEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductReportService"/> class.
        /// </summary>
        /// <param name="northwindServiceUri">An URL to Northwind OData service.</param>
        public ProductReportService(Uri northwindServiceUri)
        {
            this.entities = new NorthwindModel.NorthwindEntities(northwindServiceUri ?? throw new ArgumentNullException(nameof(northwindServiceUri)));
        }

        /// <summary>
        /// Gets a product report with all current products wit local price.
        /// </summary>
        /// <returns>Returns <see cref="ProductReport{T}"/>.</returns>
        public async Task<ProductReport<ProductLocalPrice>> GetCurrentProductsWithLocalCurrencyReport(ICountryCurrencyService countryCurrencyService, ICurrencyExchangeService currencyExchangeService)
        {
            var query = (DataServiceQuery<ProductPriceSupplier>)(
                from p in this.entities.Products
                where !p.Discontinued
                orderby p.ProductName
                select new ProductPriceSupplier
                {
                    Name = p.ProductName,
                    Price = p.UnitPrice ?? 0,
                    SupplierId = p.SupplierID,
                });

            var result = (await Task<IEnumerable<ProductPriceSupplier>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            })).ToArray();

            var products = result.ToArray();

            var localPrices = new List<ProductLocalPrice>();

            foreach (var product in products)
            {
                var supplierId = product.SupplierId;

                var supplierQuery = (DataServiceQuery<NorthwindModel.Supplier>)this.entities.Suppliers.Where(s => s.SupplierID == supplierId);

                var supplierResult = await Task<IEnumerable<NorthwindModel.Supplier>>.Factory.FromAsync(supplierQuery.BeginExecute(null, null), (ar) =>
                {
                    return supplierQuery.EndExecute(ar);
                });

                var supplier = supplierResult.Single();

                var localCurrency = await countryCurrencyService.GetLocalCurrencyByCountry(supplier.Country);
                var exchangeRate = await currencyExchangeService.GetCurrencyExchangeRate("USD", localCurrency.CurrencyCode);

                localPrices.Add(new ProductLocalPrice
                {
                    Name = product.Name,
                    Price = product.Price,
                    Country = localCurrency.CountryName,
                    CurrencySymbol = localCurrency.CurrencySymbol,
                    LocalPrice = product.Price * exchangeRate,
                });
            }

            return new ProductReport<ProductLocalPrice>(localPrices);
        }

        /// <summary>
        /// Gets a product report with all current products.
        /// </summary>
        /// <returns>Returns <see cref="ProductReport{T}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetCurrentProductsReport()
        {
            var query = (DataServiceQuery<ProductPrice>)(
                from p in this.entities.Products
                where !p.Discontinued
                orderby p.ProductName
                select new ProductPrice
                {
                    Name = p.ProductName,
                    Price = p.UnitPrice ?? 0,
                });

            var result = await Task<IEnumerable<ProductPrice>>.Factory.FromAsync(query.BeginExecute(null, null), (ar) =>
            {
                return query.EndExecute(ar);
            });

            return new ProductReport<ProductPrice>(result);
        }

        /// <summary>
        /// Gets a product report with most expensive products.
        /// </summary>
        /// <param name="count">Items count.</param>
        /// <returns>Returns <see cref="ProductReport{ProductPrice}"/>.</returns>
        public async Task<ProductReport<ProductPrice>> GetMostExpensiveProductsReport(int count)
        {
            return new ProductReport<ProductPrice>(Array.Empty<ProductPrice>());
        }

        private class ProductPriceSupplier
        {
            public string Name { get; set; }

            public decimal Price { get; set; }

            public int? SupplierId { get; set; }
        }
    }
}
