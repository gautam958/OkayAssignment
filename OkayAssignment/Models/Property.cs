using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OkayAssignment.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required]
        [Display(Name = "Property Name")]
        public string PropertyName { get; set; }
       
        [Display(Name = "No of Bedroom")]
        public int Bedroom { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvaliable { get; set; }
        
        [Display(Name = "Sale Price")]
        public Double SalePrice { get; set; }
       
        [Display(Name = "Lease Price")]
        public Double LeasePrice { get; set; }

        [Display(Name = "User Name")]
        public string UserId { get; set; }

    }
}
