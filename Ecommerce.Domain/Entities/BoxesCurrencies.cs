using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class BoxesCurrencies : BaseEntity
    {
        public int Id { get; set; }
        public float StartValue { get; set; } = 0;
        public int BoxId { get; set; }
        public Boxes Boxes { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currencies { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
