using SBC_ESTORE.Shared.DTO.Category;
using SBC_ESTORE.Shared.DTO.Product;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<GeneralResponse> AddCategory(CategoryDTO category);
        Task<DataResponse<List<CategoryDTO>>> GetAllCategory();
    }
}
