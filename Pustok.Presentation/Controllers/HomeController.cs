using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Services.Abstractions;
using Pustok.Business.ViewModels;
using Pustok.Presentation.Models;
using System.Diagnostics;

namespace Pustok.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string FOLDER_PATH;

    public HomeController(IProductService productService, IWebHostEnvironment webHostEnvironment)
    {
        _productService = productService;
        _webHostEnvironment = webHostEnvironment;
        FOLDER_PATH = Path.Combine(_webHostEnvironment.WebRootPath, "img");
    }


    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();

        return Json(products);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateViewModel vm)
    {
        var result = await _productService.CreateAsync(vm, ModelState, FOLDER_PATH);

        if(result)
            return RedirectToAction("Index");   

        return View(vm);
    }


    public async Task<IActionResult> Update(int id)
    {
        var result=await _productService.GetUpdatedProductAsync(id);

        if (result is null)
            return NotFound();

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProductUpdateViewModel vm)
    {
        var result=await _productService.UpdateAsync(vm,ModelState, FOLDER_PATH);

        if (result is false)
            return View(vm);
        else if(result is null)
            return BadRequest();

        return RedirectToAction("Index");
    }

}
