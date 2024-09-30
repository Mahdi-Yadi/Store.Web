using DataLayer.Contexts;
using DataLayer.Entities.Products;
using Domain.Products;
using Microsoft.AspNetCore.Http;
namespace Application.Services.Products;
public class ProductService : IProductService
{

    private readonly DBContext _dbContext;

    public ProductService(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool CreateProduct(CreateProductDTO dto, IFormFile imageFile)
    {
        Product product = new Product();

        product.CreateDate = DateTime.Now;
        product.Description = dto.Description;
        product.Title = dto.Title;
        product.Price = dto.Price;

        if (imageFile != null)
        {
            var imageName = Guid.NewGuid().ToString("N") + imageFile.FileName;
            string path = "wwwroot/images/";
            var image = path + imageName;

            using (var stream = new FileStream(image,FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            product.ImageName = imageName;
        }
        else
        {
            product.ImageName = "No-Image";
        }

        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
        return true;
    }

}