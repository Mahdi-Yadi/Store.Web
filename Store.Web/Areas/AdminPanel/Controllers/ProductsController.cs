using Application.Services.Products;
using DataLayer.Contexts;
using Domain.Products;
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
            return Redirect(nameof(ProductsList));
        }
        else
        {
            ViewBag.Mes = "محصول جدید ایجاد نشد";
            return Redirect(nameof(ProductsList));
        }
    }

    [HttpGet("EditProduct")]
    public IActionResult EditProduct()
    {
   
        return View();
    }

    [HttpGet("DeleteProduct")]
    public IActionResult DeleteProduct()
    {
        return View();
    }

}