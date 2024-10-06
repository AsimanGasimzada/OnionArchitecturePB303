using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Services.Abstractions;
using Pustok.Business.Utilities;
using Pustok.Business.ViewModels;
using Pustok.Core.Entities;
using Pustok.DataAccess.Repositories.Abstractions;

namespace Pustok.Business.Services.Implementations;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;



    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> CreateAsync(ProductCreateViewModel vm, ModelStateDictionary ModelState, string folderPath)
    {
        if (!ModelState.IsValid)
            return false;


        if (!vm.Image.CheckSize(2))
        {

            ModelState.AddModelError("Image", "Max image size-2mb");
            return false;
        }

        if (!vm.Image.CheckType())
        {
            ModelState.AddModelError("Image", "Please enter valid input");
            return false;

        }


        string filename = await vm.Image.CreateFileAsync(folderPath);

        Product product = new()
        {
            Name = vm.Name,
            ImagePath = filename,
            Price = vm.Price,
        };


        await _productRepository.CreateAsync(product);
        await _productRepository.SaveChangesAsync();


        return true;

    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await _productRepository.GetAll().ToListAsync();



        return products;
    }

    public async Task<ProductUpdateViewModel?> GetUpdatedProductAsync(int id)
    {
        var product = await _productRepository.GetAsync(x => x.Id == id);

        if (product is null)
            return null;

        ProductUpdateViewModel vm = new()
        {
             Name= product.Name,
              Price= product.Price,
              Id=id
        };

        return vm;
    }

    public async Task<bool?> UpdateAsync(ProductUpdateViewModel vm, ModelStateDictionary ModelState, string folderPath)
    {

        if (!ModelState.IsValid)
            return false;

        var existProduct = await _productRepository.GetAsync(x => x.Id == vm.Id);

        if (existProduct is null)
            return null;


        existProduct.Name = vm.Name;
        existProduct.Price = vm.Price;

        if(vm.Image is { })
        {
            string existImagePath=Path.Combine(folderPath, existProduct.ImagePath);
            existImagePath.DeleteFile();


            string filename = await vm.Image.CreateFileAsync(folderPath);
            existProduct.ImagePath = filename;
        }

        _productRepository.Update(existProduct);
        await _productRepository.SaveChangesAsync();

        return true;
    }
}
