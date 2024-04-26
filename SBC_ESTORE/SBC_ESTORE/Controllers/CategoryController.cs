using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBC_ESTORE.Services.CategoryServices;
using SBC_ESTORE.Services.UserService;
using SBC_ESTORE.Shared.DTO.Category;
using System.Net;

namespace SBC_ESTORE.Controllers
{
    [Route("api/admin/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult> AddCategoryController(CategoryDTO category)
        {
            var response = await categoryService.AddCategory(category);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategoryController()
        {
            var response = await categoryService.GetAllCategory();

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Data);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryController(int Id)
        {
            var response = await categoryService.DeleteCategory(Id);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryController(int Id, CategoryDTO category)
        {
            var response = await categoryService.UpdateCategory(Id, category);

            switch (response.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(response.Message);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return BadRequest("Error Occured, Try Again Later");
            }
        }
    }
}
