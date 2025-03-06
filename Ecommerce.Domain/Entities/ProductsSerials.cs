using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities
{
    public class ProductsSerials : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "المنتج")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Display(Name = "السيريال ")]
        public string SerialNumber { get; set; }
        //public int PurchaseId { get; set; }
        //[ForeignKey("PurchaseId")]
        //public Purchases Purchases { get; set; }
        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        public int InstId { get; set; } = 1;

        public Boolean Status { get; set; } = true;
        [Display(Name = "هل فعال؟")]
        public Boolean IsSold { get; set; } = false;

        //public int? SalesId { get; set; }
        //[ForeignKey("SalesId")]
        //public Sales Sales { get; set; }


    }
}
