using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OkayAssignment.Models
{
    public class Transaction
    {
        [Key]
        [Display(Name = "Transaction ID")]
        public int TransactionId { get; set; }


        
        //[Required(ErrorMessage = "Please select Property"), Range(1, int.MaxValue, ErrorMessage = "Please choose Property")]
        [Display(Name = "Property ID")]
        public int PropertyId { get; set; }

        // [NotMapped]
        //public string PropertyName { get; set; }

        [Display(Name = "User Name")]
        public string UserId { get; set; }

        [Display(Name = "Transaction Date time")]
        public DateTime TransactionDate { get; set; }
       
        //[Required(ErrorMessage = "Please select Property")]
        public virtual Property property { get; set; }

       

    }
}
