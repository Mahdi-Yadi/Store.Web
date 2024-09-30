using Domain.Products;
using Microsoft.AspNetCore.Http;
namespace Application.Services.Products;
public interface IProductService
{

    bool CreateProduct(CreateProductDTO dto,IFormFile imageFile);

}