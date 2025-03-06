using Ecommerce.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities;
public class Product : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; } //100
    public string Slug { get; set; }
    public string? KeySpecs { get; set; }
    public string? ShortDescription { get; set; } //200
    public string? Description { get; set; }
    public string? VariableTheme { get; set; }
    public float SalePrice { get; set; } = 0;
    public Boolean IsSerial { get; set; } = false;
    public Boolean IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<CustomerReview> CustomerReviews { get; set; } = new List<CustomerReview>();
    //public virtual List<ProductsSerials> ProductsSerials { get; set; } = new List<ProductsSerials>();
    //[NotMapped]
    //public string[] selectedSerials { get; set; }

}
