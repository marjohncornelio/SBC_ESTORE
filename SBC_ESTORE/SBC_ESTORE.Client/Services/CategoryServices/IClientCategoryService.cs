using SBC_ESTORE.Shared.DTO.Category;

namespace SBC_ESTORE.Client.Services.CategoryServices
{
    public interface IClientCategoryService
    {
        Task AddCategory(CategoryDTO category);
        Task<List<CategoryDTO>?> GetAllCategory();
    }
}
