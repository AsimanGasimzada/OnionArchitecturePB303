using Pustok.Core.Entities.Common;

namespace Pustok.Core.Entities;

public class Product:BaseEntity
{
    public string Name { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public decimal Price { get; set; }
}
