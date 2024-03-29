﻿using FluentValidation;

namespace FoodVault.Modules.Storage.Application.Products.AddProductImage
{
    /// <summary>
    /// Command validator for <see cref="AddProductImageCommandValidator"/>.
    /// </summary>
    internal class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductImageCommandValidator" /> class.
        /// </summary>
        public AddProductImageCommandValidator()
        {
            RuleFor(x => x.ImageId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
