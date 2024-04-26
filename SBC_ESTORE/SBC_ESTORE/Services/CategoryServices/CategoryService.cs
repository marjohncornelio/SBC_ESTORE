using Microsoft.EntityFrameworkCore;
using SBC_ESTORE.Data;
using SBC_ESTORE.Models;
using SBC_ESTORE.Services.ServiceResponse;
using SBC_ESTORE.Shared.DTO.Category;
using SBC_ESTORE.Shared.DTO.Product;
using System.Net;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext context;

        public CategoryService(DataContext context)
        {
            this.context = context;
        }

        public async Task<GeneralResponse> AddCategory(CategoryDTO category)
        {
            if (category == null)
                return new GeneralResponse("Category is Empty", HttpStatusCode.BadRequest);

            var newCategory = new Category()
            {
                Name = category.Name,
            };

            context.Categories.Add(newCategory);
            await context.SaveChangesAsync();
                return new GeneralResponse("Category is successfully Added");

        }

        public async Task<DataResponse<List<CategoryDTO>>> GetAllCategory()
        {
            var response = await context.Categories.ToListAsync();
            if(response == null)
                return new DataResponse<List<CategoryDTO>>(null!, "No existing category", HttpStatusCode.NotFound);

            var categories = response.Select(p => new CategoryDTO
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();

            return new DataResponse<List<CategoryDTO>>(categories, "Category Fetched");

        }
        public async Task<GeneralResponse> DeleteCategory(int Id)
        {
            var response = await context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            if (response == null)
                return new GeneralResponse("No existing category", HttpStatusCode.NotFound);

            context.Categories.Remove(response);
            await context.SaveChangesAsync();
            
            return new GeneralResponse("Category Deleted");
        }
        public async Task<GeneralResponse> UpdateCategory(int Id, CategoryDTO category)
        {
            var response = await context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            if (response == null)
                return new GeneralResponse("No existing category", HttpStatusCode.NotFound);

            response.Name = category.Name;
            await context.SaveChangesAsync();

            return new GeneralResponse("Category Updated");
        }

    }
}
