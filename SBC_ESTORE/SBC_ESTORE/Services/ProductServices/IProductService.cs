using SBC_ESTORE.Shared.DTO.Product;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.ProductServices
{
    public interface IProductService
    {
        Task<GeneralResponse> AddProduct(ProductDTO product);
        Task<GeneralResponse> UpdateProduct(ProductDTO product, int Id);
        Task<GeneralResponse> DeleteProduct(int Id);
        Task<DataResponse<ProductDTO>> GetProductById(int Id);
        Task<DataResponse<List<ProductDTO>>> GetAllProducts();
    }
}
