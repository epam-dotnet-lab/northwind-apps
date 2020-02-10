// ReSharper disable CheckNamespace
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Northwind.DataAccess;
using Northwind.DataAccess.Products;

namespace Northwind.Services.Products
{
    /// <summary>
    /// Represents a management service for products and product categories.
    /// </summary>
    public sealed class ProductManagementService : IProductCategoryManagementService, IProductManagementService
    {
        private readonly NorthwindDataAccessFactory daoFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductManagementService"/> class.
        /// </summary>
        /// <param name="daoFactory">A <see cref="NorthwindDataAccessFactory"/> that produces data access objects.</param>
        public ProductManagementService(NorthwindDataAccessFactory daoFactory)
        {
            this.daoFactory = daoFactory ?? throw new ArgumentNullException(nameof(daoFactory));
        }

        /// <inheritdoc/>
        IList<ProductCategory> IProductCategoryManagementService.Show(int offset, int limit)
        {
            var dao = this.daoFactory.GetProductCategoryDataAccessObject();
            var productCategoriesTo = dao.SelectProductCategories(offset, limit);
            var list = productCategoriesTo.Select(c => new ProductCategory
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            });

            return list.ToList();
        }

        /// <inheritdoc/>
        bool IProductCategoryManagementService.TryShow(int categoryId, out ProductCategory productCategory)
        {
            var dao = this.daoFactory.GetProductCategoryDataAccessObject();

            ProductCategoryTransferObject productCategoryTo;
            try
            {
                productCategoryTo = dao.FindProductCategory(categoryId);
            }
            catch (ProductCategoryNotFoundException)
            {
                // TODO write to log.
                productCategory = null;
                return false;
            }

            productCategory = new ProductCategory
            {
                Id = productCategoryTo.Id,
                Name = productCategoryTo.Name,
                Description = productCategoryTo.Description,
            };
            return true;
        }

        /// <inheritdoc/>
        int IProductCategoryManagementService.Create(ProductCategory productCategory)
        {
            if (productCategory == null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }

            var dao = this.daoFactory.GetProductCategoryDataAccessObject();
            var id = dao.InsertProductCategory(new ProductCategoryTransferObject
            {
                Name = productCategory.Name,
                Description = productCategory.Description,
            });

            return id;
        }

        /// <inheritdoc/>
        bool IProductCategoryManagementService.Destroy(int categoryId)
        {
            var dao = this.daoFactory.GetProductCategoryDataAccessObject();
            return dao.DeleteProductCategory(categoryId);
        }

        /// <inheritdoc/>
        IList<ProductCategory> IProductCategoryManagementService.LookupByName(IList<string> names)
        {
            if (names == null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            var dao = this.daoFactory.GetProductCategoryDataAccessObject();
            var productCategoriesTo = dao.SelectProductCategoriesByName(names);
            var list = productCategoriesTo.Select(c => new ProductCategory
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            });

            return list.ToList();
        }

        /// <inheritdoc/>
        bool IProductCategoryManagementService.Update(int categoryId, ProductCategory productCategory)
        {
            if (productCategory == null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }

            var dao = this.daoFactory.GetProductCategoryDataAccessObject();
            return dao.UpdateProductCategory(new ProductCategoryTransferObject
            {
                Id = categoryId,
                Name = productCategory.Name,
                Description = productCategory.Description,
                Picture = null,
            });
        }

        /// <inheritdoc />
        bool IProductCategoryManagementService.TryShowPicture(int categoryId, out byte[] bytes)
        {
            const int oleHeaderSize = 78; // Images were uploaded in the old years for MS Access, and they have OLE header before image bytes.

            var dao = this.daoFactory.GetProductCategoryDataAccessObject();

            ProductCategoryTransferObject productCategoryTo;
            try
            {
                productCategoryTo = dao.FindProductCategory(categoryId);
            }
            catch (ProductCategoryNotFoundException)
            {
                bytes = null;
                return false;
            }

            byte[] picture;

            if (productCategoryTo.Picture != null && HasOleSignature(productCategoryTo.Picture))
            {
                picture = new byte[productCategoryTo.Picture.Length - oleHeaderSize];
                Array.Copy(productCategoryTo.Picture, oleHeaderSize, picture, 0, productCategoryTo.Picture.Length - oleHeaderSize);
            }
            else
            {
                picture = productCategoryTo.Picture;
            }

            bytes = picture;
            return true;
        }

        /// <inheritdoc />
        bool IProductCategoryManagementService.DestroyPicture(int categoryId)
        {
            var dao = this.daoFactory.GetProductCategoryDataAccessObject();

            ProductCategoryTransferObject productCategoryTo;
            try
            {
                productCategoryTo = dao.FindProductCategory(categoryId);
            }
            catch (ProductCategoryNotFoundException)
            {
                return false;
            }

            productCategoryTo.Picture = null;
            dao.UpdateProductCategory(productCategoryTo);
            return true;
        }

        /// <inheritdoc />
        bool IProductCategoryManagementService.UpdatePicture(int categoryId, Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var dao = this.daoFactory.GetProductCategoryDataAccessObject();

            ProductCategoryTransferObject productCategoryTo;
            try
            {
                productCategoryTo = dao.FindProductCategory(categoryId);
            }
            catch (ProductCategoryNotFoundException)
            {
                return false;
            }

            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            productCategoryTo.Picture = buffer;
            dao.UpdateProductCategory(productCategoryTo);

            return true;
        }

        /// <inheritdoc />
        IList<Product> IProductManagementService.Show(int offset, int limit)
        {
            var dao = this.daoFactory.GetProductDataAccessObject();
            var productsTo = dao.SelectProducts(offset, limit);
            var list = productsTo.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                SupplierId = p.SupplierId,
                CategoryId = p.CategoryId,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                ReorderLevel = p.ReorderLevel,
                Discontinued = p.Discontinued,
            });

            return list.ToList();
        }

        /// <inheritdoc />
        bool IProductManagementService.TryShow(int productId, out Product product)
        {
            var dao = this.daoFactory.GetProductDataAccessObject();

            ProductTransferObject productTo;
            try
            {
                productTo = dao.FindProduct(productId);
            }
            catch (ProductNotFoundException)
            {
                // TODO write to log.
                product = null;
                return false;
            }

            product = new Product
            {
                Id = productTo.Id,
                Name = productTo.Name,
                SupplierId = productTo.SupplierId,
                CategoryId = productTo.CategoryId,
                QuantityPerUnit = productTo.QuantityPerUnit,
                UnitPrice = productTo.UnitPrice,
                UnitsInStock = productTo.UnitsInStock,
                UnitsOnOrder = productTo.UnitsOnOrder,
                ReorderLevel = productTo.ReorderLevel,
                Discontinued = productTo.Discontinued,
            };
            return true;
        }

        /// <inheritdoc />
        int IProductManagementService.Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var dao = this.daoFactory.GetProductDataAccessObject();
            var id = dao.InsertProduct(new ProductTransferObject
            {
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            });

            return id;
        }

        /// <inheritdoc />
        bool IProductManagementService.Destroy(int productId)
        {
            var dao = this.daoFactory.GetProductDataAccessObject();
            return dao.DeleteProduct(productId);
        }

        /// <inheritdoc />
        public IList<Product> LookupByName(IList<string> names)
        {
            if (names == null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            var dao = this.daoFactory.GetProductDataAccessObject();
            var productsTo = dao.SelectProductsByName(names);
            var list = productsTo.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                SupplierId = p.SupplierId,
                CategoryId = p.CategoryId,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                UnitsOnOrder = p.UnitsOnOrder,
                ReorderLevel = p.ReorderLevel,
                Discontinued = p.Discontinued,
            });

            return list.ToList();
        }

        /// <inheritdoc />
        bool IProductManagementService.Update(int productId, Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var dao = this.daoFactory.GetProductDataAccessObject();
            return dao.UpdateProduct(new ProductTransferObject
            {
                Id = productId,
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            });

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IList<Product> IProductManagementService.ShowForCategory(int categoryId)
        {
            var dao = this.daoFactory.GetProductDataAccessObject();
            var result = dao.SelectProductByCategory(new[] { categoryId });
            return result.Select(c => new Product
            {
                Id = c.Id,
                Name = c.Name,
                SupplierId = c.SupplierId,
                CategoryId = c.CategoryId,
                QuantityPerUnit = c.QuantityPerUnit,
                UnitPrice = c.UnitPrice,
                UnitsInStock = c.UnitsInStock,
                UnitsOnOrder = c.UnitsOnOrder,
                ReorderLevel = c.ReorderLevel,
                Discontinued = c.Discontinued,
            }).ToArray();
        }

        private static bool HasOleSignature(IReadOnlyList<byte> buffer)
        {
            return buffer[0] == 0x15 && buffer[1] == 0x1C;
        }
    }
}
