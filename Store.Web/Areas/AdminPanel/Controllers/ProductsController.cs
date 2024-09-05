using DataLayer.Contexts;
using Microsoft.AspNetCore.Mvc;
namespace Store.Web.Areas.AdminPanel.Controllers;
public class ProductsController : AdminBaseController
{

    private readonly DBContext _dbContext;

    public ProductsController(DBContext dbContext)
    {
        _dbContext = dbContext;
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