using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dto
{
    public class StoreDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int CityId { get; set; }
        public City Cities { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String TelPhone { get; set; }
        public String Description { get; set; }
        public Boolean Status { get; set; } = true;
    }
}
