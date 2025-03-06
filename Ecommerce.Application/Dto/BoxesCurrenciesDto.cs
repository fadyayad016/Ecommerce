using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dto
{
    public class BoxesCurrenciesDto
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
