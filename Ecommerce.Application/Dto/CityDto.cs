using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dto;

    public class CityDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public string Description { get; set; }
        public Boolean Status { get; set; }
    }

