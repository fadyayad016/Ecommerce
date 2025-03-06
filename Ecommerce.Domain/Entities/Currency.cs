using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class Currency :BaseEntity
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Symbol { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
