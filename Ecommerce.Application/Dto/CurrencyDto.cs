using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dto
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Symbol { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
