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

    List<ProductDto> GetProductsHasDiscount();

    List<ProductDto> GetSpecialProducts();

    List<ProductDto> GetPopularProducts();

    ProductDto GetProductDetail(int id);

    bool AddDiscount(Discount dis);

    bool DeleteDiscount(int id);

    void UpdateVisit(int id);

    bool AddProductToFav(int id, int userId);

    List<ProductDto> GetFavProducts(int userId);

    bool DeleteProductToFav(int id, int userId);

}