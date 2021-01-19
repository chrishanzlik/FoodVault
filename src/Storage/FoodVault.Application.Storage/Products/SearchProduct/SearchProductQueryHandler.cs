using FoodVault.Application.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.Products.SearchProduct
{
    /// <summary>
    /// Query handler for <see cref="SearchProductQuery"/>.
    /// </summary>
    public class SearchProductQueryHandler : IQueryHandler<SearchProductQuery, ProductDto>
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
