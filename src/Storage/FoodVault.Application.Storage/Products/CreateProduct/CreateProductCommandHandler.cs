using FoodVault.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodVault.Application.Storage.Products.CreateProduct
{
    /// <summary>
    /// Command handler for <see cref="CreateProductCommand"/>.
    /// </summary>
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        public Task<ICommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
