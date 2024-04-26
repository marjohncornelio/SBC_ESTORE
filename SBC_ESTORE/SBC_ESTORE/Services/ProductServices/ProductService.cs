using Azure;
using Microsoft.EntityFrameworkCore;
using SBC_ESTORE.Data;
using SBC_ESTORE.Models;
using SBC_ESTORE.Services.ServiceResponse;
using SBC_ESTORE.Shared.DTO.Category;
using SBC_ESTORE.Shared.DTO.Product;
using System.Net;
using static SBC_ESTORE.Services.ServiceResponse.Response;

namespace SBC_ESTORE.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DataContext context;

        public ProductService(DataContext context)
        {
            this.context = context;
        }

        public async Task<GeneralResponse> AddProduct(ProductDTO product)
        {
            if (product == null)
                return new GeneralResponse("Product is Empty", HttpStatusCode.BadRequest);

            var newcategory = new Category();
            if (product.ProductCategory != null)
            {
                newcategory = new Category(){
                    Id = product.ProductCategory.Id,
                    Name = product.ProductCategory.Name,
                };
            }
 
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,  
                Price = product.Price,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl,
                CategoryId = newcategory.Id,
            };

            context.Products.Add(newProduct);
            await context.SaveChangesAsync();
            return new GeneralResponse("Product is successfully Added");
        }

        public async Task<GeneralResponse> DeleteProduct(int Id)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == Id);
            if (product == null)
                return new GeneralResponse("Product Not Found", HttpStatusCode.NotFound);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return new GeneralResponse("Product Deleted Successfully");
        }

        public async Task<DataResponse<List<ProductDTO>>> GetAllProducts()
        {
            var products = await context.Products.ToListAsync();
            if (products == null)
                return new DataResponse<List<ProductDTO>>(null!, "No Data Found", HttpStatusCode.NotFound);

            var productList = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
            }).ToList();
            return new DataResponse<List<ProductDTO>>(productList, "Products Fetched");
        }

        public async Task<DataResponse<ProductDTO>> GetProductById(int Id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (product == null)
                return new DataResponse<ProductDTO>(null!, "No Data Found", HttpStatusCode.NotFound);

            var categoryDTO = new CategoryDTO(); // Create a new CategoryDTO object

            if (product != null)
            {
                var category = await context.Categories.FindAsync(product.CategoryId);
                if (category != null)
                {
                    categoryDTO.Id = category.Id;
                    categoryDTO.Name = category.Name;
                }
            }

            var productDTO = new ProductDTO
            {
                Id = product!.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                ProductCategory = categoryDTO,
            };

            return new DataResponse<ProductDTO>(productDTO, "Data Fetched");
        }

        public async Task<GeneralResponse> UpdateProduct(ProductDTO Product, int Id)
        {
            var response = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (response == null)
                return new GeneralResponse("No Data Found", HttpStatusCode.NotFound);

            response.Name = Product.Name;
            response.Description = Product.Description;
            response.Price = Product.Price;
            response.Quantity = Product.Quantity;
            response.ImageUrl = Product.ImageUrl;
            response.CategoryId = Product.CategoryId;

            await context.SaveChangesAsync();
            return new GeneralResponse("Product Updated Successfully");
        }
    }
}
