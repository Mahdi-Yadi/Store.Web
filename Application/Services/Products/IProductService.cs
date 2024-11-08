using DataLayer.Entities.Categories;
using Domain.Products;
using Microsoft.AspNetCore.Http;
namespace Application.Services.Products;
public interface IProductService
{

    bool CreateProduct(CreateProductDTO dto,IFormFile imageFile);

    EditProductDTO GetProduct(int id);

    bool EditProduct(EditProductDTO dto,IFormFile imageFile);

    bool DeleteProduct(int id);

    List<Category> GetCategories();

}