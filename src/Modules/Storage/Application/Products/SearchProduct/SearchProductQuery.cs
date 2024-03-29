﻿
using FoodVault.Framework.Application.Queries;

namespace FoodVault.Modules.Storage.Application.Products.SearchProduct
{
    /// <summary>
    /// Query that searches for a product.
    /// </summary>
    public class SearchProductQuery : IQuery<ProductDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchProductQuery" /> class.
        /// </summary>
        public SearchProductQuery(string barcode)
        {
            Barcode = barcode;
        }

        /// <summary>
        /// Gets the barcode of the product to search.
        /// </summary>
        public string Barcode { get; }
    }
}
