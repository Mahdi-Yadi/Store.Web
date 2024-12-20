﻿using DataLayer.Contexts;
using DataLayer.Entities.Categories;
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

            using (var stream = new FileStream(image, FileMode.Create))
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

        if (dto.catsid != null)
        {
            foreach (var id in dto.catsid)
            {
                ProductsCategories category = new ProductsCategories();
                category.CategoryId = id;
                category.ProductId = product.Id;
                _dbContext.ProductsCategories.Add(category);
            }
            _dbContext.SaveChanges();
        }

        return true;
    }

    public EditProductDTO GetProduct(int id)
    {
        var product = _dbContext.Products.SingleOrDefault(x => x.Id == id);

        EditProductDTO dto = new EditProductDTO();

        if (product != null)
        {
            dto.ImageName = product.ImageName;
            dto.Description = product.Description;
            dto.Title = product.Title;
            dto.Price = product.Price;
            dto.ProductId = product.Id;
        }

        return dto;
    }

    public bool EditProduct(EditProductDTO dto, IFormFile imageFile)
    {
        var product = _dbContext.Products.SingleOrDefault(p => p.Id == dto.ProductId);

        product.Title = dto.Title;
        product.Description = dto.Description;
        product.Price = dto.Price;

        if (imageFile != null)
        {
            if (product.ImageName != null)
            {
                var pathOld = Path.Combine("wwwroot/images/" + product.ImageName);
                if (File.Exists(pathOld))
                {
                    File.Delete(pathOld);
                }
            }

            var imageName = Guid.NewGuid().ToString("N") + imageFile.FileName;
            string path = "wwwroot/images/";
            var image = path + imageName;

            using (var stream = new FileStream(image, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            product.ImageName = imageName;
        }
        else
        {
            product.ImageName = dto.ImageName;
        }

        _dbContext.Products.Update(product);
        _dbContext.SaveChanges();

        return true;
    }

    public bool DeleteProduct(int id)
    {
        var product = _dbContext.Products.SingleOrDefault(p => p.Id == id);

        if (product != null)
        {
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }

    public List<Category> GetCategories()
    {
        return _dbContext
            .Categories.ToList();
    }

}