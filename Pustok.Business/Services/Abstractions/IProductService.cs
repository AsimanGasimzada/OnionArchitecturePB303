using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pustok.Business.ViewModels;
using Pustok.Core.Entities;

namespace Pustok.Business.Services.Abstractions;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();


    Task<bool> CreateAsync(ProductCreateViewModel vm, ModelStateDictionary ModelState,string folderPath);

    Task<ProductUpdateViewModel?> GetUpdatedProductAsync(int id);
    Task<bool?> UpdateAsync(ProductUpdateViewModel vm, ModelStateDictionary ModelState, string folderPath);
}
