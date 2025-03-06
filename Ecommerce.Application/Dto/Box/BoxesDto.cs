using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dto.Box
{
    public class BoxesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<BoxesCurrenciesDto> BoxesCurrencies { get; set; } = new List<BoxesCurrenciesDto>();
    }
}
