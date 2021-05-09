using System;

namespace FoodVault.Modules.Storage.Application.FoodStorages.GetStorageContent
{
    public class StoredProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Barcode { get; set; }
        public int Quantity { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string ImageUrl { get; set; }
    }
}
