﻿using FluentValidation;

namespace FoodVault.Application.Storage.FoodStorages.RemoveProduct
{
    /// <summary>
    /// Validator for the <see cref="RemoveProductCommand"/>.
    /// </summary>
    public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveProductCommandValidator" /> class.
        /// </summary>
        public RemoveProductCommandValidator()
        {
            RuleFor(x => x.StorageId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
