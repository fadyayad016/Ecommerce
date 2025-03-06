using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Dto
{
    public class BarcodeDto
    {
        public int Id { get; set; }
        public string BarcodeName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
