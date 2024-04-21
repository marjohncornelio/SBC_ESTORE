﻿using SBC_ESTORE.Shared.DTO.Product;

namespace SBC_ESTORE.Client.Services.ProductServices
{
    public interface IClientProductService
    {
        Task AddProduct(ProductDTO product);
        Task UpdateProduct(ProductDTO product, int Id);
        Task DeleteProduct(int Id);
        Task<ProductDTO?> GetProductById(int Id);
        Task<List<ProductDTO>?> GetAllProducts();
    }
}
