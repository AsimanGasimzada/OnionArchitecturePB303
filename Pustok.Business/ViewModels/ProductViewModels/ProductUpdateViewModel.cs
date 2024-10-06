using Microsoft.AspNetCore.Http;

namespace Pustok.Business.ViewModels;

public class ProductUpdateViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public IFormFile Image { get; set; } = null!;
}
