﻿using DataLayer.Contexts;
using DataLayer.Entities.Categories;
using DataLayer.Entities.Discounts;
using DataLayer.Entities.Products;
using Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Products;
public class ProductService : IProductService
{

    private readonly DBContext _dbContext;

    public ProductService(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ProductDto GetProductDetail(int id)
    {
        var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return new ProductDto();

        ProductDto dto = new ProductDto();

        dto.Description = product.Description;
        dto.ImageName = product.ImageName;
        dto.Title = product.Title;
        dto.Id = product.Id;
        dto.Price = product.Price;
        dto.IsSpecial = product.IsSpecial;

        return dto;
    }

    public bool CreateProduct(CreateProductDTO dto, IFormFile imageFile)
    {
        Product product = new Product();

        product.CreateDate = DateTime.Now;
        product.Description = dto.Description;
        product.Title = dto.Title;
        product.Price = dto.Price;
        product.IsSpecial = dto.IsSpecial;

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
            var catsId = _dbContext.ProductsCategories
                .Where(a => a.ProductId == product.Id)
                .Select(c => c.CategoryId)
                .ToList();

            if (catsId.Count != 0)
            {
                dto.catsid = catsId;
            }

            dto.ImageName = product.ImageName;
            dto.Description = product.Description;
            dto.Title = product.Title;
            dto.Price = product.Price;
            dto.ProductId = product.Id;
            dto.IsSpecial = product.IsSpecial;
        }

        return dto;
    }

    public bool EditProduct(EditProductDTO dto, IFormFile imageFile)
    {
        var product = _dbContext.Products.SingleOrDefault(p => p.Id == dto.ProductId);

        product.Title = dto.Title;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.IsSpecial = dto.IsSpecial;

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

        if (dto.catsid != null)
        {
            var cats1 = _dbContext.ProductsCategories
                .Where(a => a.ProductId == product.Id)
                .ToList();

            _dbContext.RemoveRange(cats1);
            _dbContext.SaveChanges();

            foreach (var id in dto.catsid)
            {
                ProductsCategories category = new ProductsCategories();
                category.CategoryId = id;
                category.ProductId = product.Id;
                _dbContext.ProductsCategories.Add(category);
            }

            _dbContext.SaveChanges();
        }
        else
        {
            var cats = _dbContext.ProductsCategories.Where(
                    a => a.ProductId == product.Id)
                .ToList();
            _dbContext.ProductsCategories.RemoveRange(cats);
            _dbContext.SaveChanges();
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

            var cats = _dbContext.ProductsCategories.Where(a => a.ProductId == product.Id)
                .ToList();

            if (cats.Count != 0)
            {
                _dbContext.RemoveRange(cats);
                _dbContext.SaveChanges();
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }

    public List<ProductDto> GetLastProducts()
    {
        var products = _dbContext
            .Products
            .OrderByDescending(p=>p.CreateDate)
            .ToList();

        if (products.Count == 0)
            return new List<ProductDto>();

        List<ProductDto> dtos = new List<ProductDto>();

        foreach (var item in products)
        {
            var a = new ProductDto()
            {
                Id = item.Id,
                Description = item.Description,
                ImageName = item.ImageName,
                Price = item.Price,
                Title = item.Title
            };
            dtos.Add(a);
        }

        return dtos;
    }

    public List<ProductDto> GetSpecialProducts()
    {
        var products = _dbContext
            .Products
            .OrderByDescending(p => p.CreateDate)
            .Where(a => a.IsSpecial)
            .ToList();

        if (products.Count == 0)
            return new List<ProductDto>();

        List<ProductDto> dtos = new List<ProductDto>();

        foreach (var item in products)
        {
            var a = new ProductDto()
            {
                Id = item.Id,
                Description = item.Description,
                ImageName = item.ImageName,
                Price = item.Price,
                Title = item.Title,
                Discount = GetDiscount(item.Id)
            };
            dtos.Add(a);
        }

        return dtos;
    }

    private Discount GetDiscount(int id)
    {
        var dis = _dbContext.Discounts
            .FirstOrDefault(a => a.ExpireDate > DateTime.Now && a.IsDeleted == false && a.ProductId == id);

        if (dis == null)
            return new Discount();

        return dis;
    }

    public List<Category> GetCategories()
    {
        return _dbContext
            .Categories.ToList();
    }

    public bool AddDiscount(Discount dis)
    {
        if (dis.ProductId == 0)
            return false;

        var OldDis = _dbContext.Discounts
            .FirstOrDefault(a => a.ProductId == dis.ProductId 
                                 && a.IsDeleted == false 
                                 && a.ExpireDate > DateTime.Now);

        if (OldDis != null)
        {
            OldDis.IsDeleted = true;

            _dbContext.Discounts.Update(OldDis);
            _dbContext.SaveChanges();
        }

        Discount d = new Discount();

        d.ExpireDate = dis.ExpireDate;
        d.DiscountPercentage = dis.DiscountPercentage;
        d.IsDeleted = false;
        d.ProductId = dis.ProductId;

        _dbContext.Discounts.Add(d);
        _dbContext.SaveChanges();
        return true;
    }

    public bool DeleteDiscount(int id)
    {
        var dis = _dbContext
            .Discounts
            .FirstOrDefault(a => a.ProductId == id
            && a.ExpireDate > DateTime.Now
            && a.IsDeleted == false);

        if (dis == null)
            return true;

        dis.IsDeleted = true;

        _dbContext.Discounts.Update(dis);
        _dbContext.SaveChanges();
        return true;
    }

}