using Microsoft.AspNetCore.Http;

namespace Pustok.Business.ViewModels;

public class ProductCreateViewModel
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; } 
    public IFormFile Image { get; set; } = null!;
}
