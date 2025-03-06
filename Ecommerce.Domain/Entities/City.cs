using Ecommerce.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Ecommerce.Domain.Entities
{
    public class City : BaseEntity
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public string Description { get; set; }
        public Boolean Status { get; set; }
    }
}
