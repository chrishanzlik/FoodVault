using FoodVault.Framework.Application.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Modules.Storage.Application.Products.SearchProduct
{
    /// <summary>
    /// Query handler for <see cref="SearchProductQuery"/>.
    /// </summary>
    internal class SearchProductQueryHandler : IQueryHandler<SearchProductQuery, ProductDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchProductQueryHandler" /> class.
        /// </summary>
        public SearchProductQueryHandler()
        {

        }

        /// <inheritdoc />
        public Task<ProductDto> Handle(SearchProductQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
