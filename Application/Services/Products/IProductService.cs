using DataLayer.Entities.Categories;
using DataLayer.Entities.Discounts;
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

    List<ProductDto> GetLastProducts();

    List<ProductDto> GetSpecialProducts();

    ProductDto GetProductDetail(int id);

    bool AddDiscount(Discount dis);

    bool DeleteDiscount(int id);

}