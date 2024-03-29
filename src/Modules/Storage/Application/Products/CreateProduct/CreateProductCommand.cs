﻿using FoodVault.Framework.Application.Commands;
using System;

namespace FoodVault.Modules.Storage.Application.Products.CreateProduct
{
    /// <summary>
    /// Command for creating a product.
    /// </summary>
    public class CreateProductCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductCommand" /> class.
        /// </summary>
        /// <param name="productName">Products name / description.</param>
        /// <param name="brand">Brand of the product.</param>
        /// <param name="barcode">Products barcode.</param>
        /// <param name="imageUploadId">Products image id.</param>
        public CreateProductCommand(
            string productName,
            string brand = null,
            string barcode = null,
            Guid? imageUploadId = null)
        {
            ProductName = productName;
            Barcode = barcode;
            Brand = brand;
            ImageUploadId = imageUploadId;
        }

        /// <summary>
        /// Gets the products name.
        /// </summary>
        public string ProductName { get; }

        /// <summary>
        /// Gets the brand of the product.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Gets the products barcode.
        /// </summary>
        public string Barcode { get; }

        /// <summary>
        /// Gets the associated image id.
        /// </summary>
        public Guid? ImageUploadId { get; }
    }
}