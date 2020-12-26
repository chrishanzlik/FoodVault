﻿using FluentValidation;

namespace FoodVault.Application.Storage.FoodStorages.CreateStorage
{
    /// <summary>
    /// Validator for the <see cref="CreateStorageCommand"/>.
    /// </summary>
    public class CreateStorageCommandValidator : AbstractValidator<CreateStorageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStorageCommandValidator" /> class.
        /// </summary>
        public CreateStorageCommandValidator()
        {
            RuleFor(x => x.StorageName).NotEmpty();
        }
    }
}