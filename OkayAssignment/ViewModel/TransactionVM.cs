using OkayAssignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OkayAssignment.ViewModel
{
    public class TransactionVM
    {
        public Transaction transaction { get; set; }

        // Use for Index page display Propertyname
        [Display(Name = "Property ID")]
        public int propertyid { get; set; }
    }
}
