using Ecommerce.Domain.Identity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class Purchases
    {
        [Key]
        public int Id { get; set; }
        public string PurchasesNumber { get; set; }
        public DateTime DateOfAdd { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Store Required")]
        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Stores { get; set; }

        //public int BoxId { get; set; }
        //[ForeignKey("BoxId")]
        //public Boxes CachBox { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public Customer Suppliers { get; set; }

        public int IsCash { get; set; }
        public int InstId { get; set; }

        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currencies { get; set; }

        public String Description { get; set; }

        public Boolean Status { get; set; } = true;

        [NotMapped]
        public decimal numberOfPieces { get; set; }
        [NotMapped]
        public decimal TotalPrice { get; set; }

       // public virtual List<PurchaseBill> PurchaseBill { get; set; } = new List<PurchaseBill>();

    }
}
