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
    public class Store : BaseEntity
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int CityId { get; set; }
        public virtual City Cities { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String TelPhone { get; set; }
        public String Description { get; set; }
        public Boolean IsActive { get; set; } = true;
    }
}
