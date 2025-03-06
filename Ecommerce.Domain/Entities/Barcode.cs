using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class Barcode : BaseEntity
    {
        public int Id { get; set; }
        public string BarcodeName { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
        public Boolean IsActive { get; set; } = true;

    }
}
