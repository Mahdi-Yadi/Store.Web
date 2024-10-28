using Application.Services.Products;
using DataLayer.Contexts;
using Domain.Products;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.AdminPanel.Controllers;
public class ProductsController : AdminBaseController
{

    private readonly DBContext _dbContext;
    private readonly IProductService _productService;

    public ProductsController(DBContext dbContext, IProductService productService)
    {
        _dbContext = dbContext;
        _productService = productService;
    }

    [HttpGet("ProductsList")]
    public IActionResult ProductsList()
    {
        var products = _dbContext.Products.ToList();

        return View(products);
    }

    [HttpGet("CreateProduct")]
    public IActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost("CreateProduct")]
    public IActionResult CreateProduct(CreateProductDTO dto,IFormFile imageFile)
    {
        var res = _productService.CreateProduct(dto, imageFile);
        if (res)
        {
            ViewBag.Mes = "محصول جدید ایجاد شد";
            return Redirect("/PanelAdmin/ProductsList");
        }
        else
        {
            ViewBag.Mes = "محصول جدید ایجاد نشد";
            return Redirect("/PanelAdmin/ProductsList");
        }
    }

    [HttpGet("EditProduct/{id}")]
    public IActionResult EditProduct(int id)
    {
        var product = _productService.GetProduct(id);

        return View(product);
    }

    [HttpPost("EditProduct/{id}")]
    public IActionResult EditProduct(int id,EditProductDTO dto,IFormFile imageFile)
    {
        var res = _productService.EditProduct(dto, imageFile);

        if (res)
        {
            ViewBag.Mes = "محصول ویرایش شد";
            return Redirect("/PanelAdmin/ProductsList");
        }
        else
        {
            ViewBag.Mes = "محصول ویرایش نشد";
            return Redirect("/PanelAdmin/ProductsList");
        }
    }

    [HttpGet("DeleteProduct/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _productService.GetProduct(id);
        return View(product);
    }

    [HttpPost("DeleteProduct/{id}")]
    public IActionResult DeleteProduct(int id,EditProductDTO dto)
    {
        var res = _productService.DeleteProduct(dto.ProductId);

        if (res)
        {
            ViewBag.Mes = "محصول حذف شد";
            return Redirect("/PanelAdmin/ProductsList");
        }
        else
        {
            ViewBag.Mes = "محصول حذف نشد";
            return Redirect("/PanelAdmin/ProductsList");
        }
    }

}