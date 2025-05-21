using Application.Services.Products;
using DataLayer.Contexts;
using DataLayer.Entities.Discounts;
using DataLayer.Entities.Products;
using Domain.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var products = _dbContext
            .Products
            .Include(a => a.Discounts.Where(a => a.IsDeleted == false && a.ExpireDate > DateTime.Now))
            .ToList();

        return View(products);
    }

    [HttpGet("CreateProduct")]
    public IActionResult CreateProduct()
    {
        ViewBag.cats = _productService.GetCategories();
        return View();
    }

    [HttpPost("CreateProduct")]
    public IActionResult CreateProduct(CreateProductDTO dto,IFormFile imageFile)
    {
        var res = _productService.CreateProduct(dto, imageFile);
        ViewBag.cats = _productService.GetCategories();
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
        ViewBag.cats = _productService.GetCategories();
        return View(product);
    }

    [HttpPost("EditProduct/{id}")]
    public IActionResult EditProduct(int id,EditProductDTO dto,IFormFile imageFile)
    {
        ViewBag.cats = _productService.GetCategories();
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

    [HttpGet("Discount/{id}")]
    public IActionResult Discount(int id)
    {
        Discount dis = new Discount();

        dis.ProductId = id;

        return View(dis);
    }

    [HttpPost("Discount/{id}")]
    public IActionResult Discount(Discount dis, int id)
    {
        dis.ProductId = id;

        if (ModelState.IsValid)
        {
            var res = _productService.AddDiscount(dis);
            if (res)
            {
                ViewBag.Mes = "تخفیف اضافه شد";
                return Redirect("/PanelAdmin/ProductsList");
            }
            else
            {
                ViewBag.Mes = "تخفیف اضافه نشد";
                return Redirect("/PanelAdmin/ProductsList");
            }
        }

        return View(dis);
    }

    [HttpGet("DeleteDiscount/{id}")]
    public IActionResult DeleteDiscount(int id,string title)
    {
        ViewBag.Producttitle = title;
        ViewBag.id = id;
        return View();
    }

    [HttpPost("DeleteDiscount/{id}")]
    public IActionResult DeleteDiscount(int id)
    {
        var res = _productService.DeleteDiscount(id);

        if (res)
        {
            ViewBag.Mes = "تخفیف حذف شد";
            return Redirect("/PanelAdmin/ProductsList");
        }
        else
        {
            ViewBag.Mes = "تخفیف حذف نشد";
            return Redirect("/PanelAdmin/ProductsList");
        }
    }

    [HttpGet("ColorsList/{productId}")]
    public IActionResult ColorsList(long productId)
    {
        var colors = _productService.GetColorProducts(productId);

        ViewBag.productId = productId;

        return View(colors);
    }

    [HttpGet("AddColor/{productId}")]
    public IActionResult AddColor(long productId)
    {
        return View();
    }

    [HttpPost("AddColor/{productId}")]
    public IActionResult AddColor(ColorProduct c ,long productId)
    {
        c.ProductId = productId;
        var res = _productService.AddColor(c);

        switch (res)
        {
            case ColorResult.Null:
                TempData[WarningMessage] = "اطلاعات کامل نیست";
                break;
            case ColorResult.Error:
                TempData[ErrorMessage] = "خطایی رخ داد";
                break;
            case ColorResult.IsExist:
                TempData[WarningMessage] = "رنگ ثبت شده!";
                break;
            case ColorResult.Success:
                TempData[SuccessMessage] = "رنگ جدید با موفقیت ثبت شد";
                return Redirect($"/PanelAdmin/AddColor/{c.ProductId}");
        }

        return View();
    }

    [HttpGet("UpdateColor/{id}")]
    public IActionResult UpdateColor(long id)
    {

        return View();
    }

    [HttpGet("DeleteColor/{id}")]
    public IActionResult DeleteColor(long id)
    {

        return View();
    }

}