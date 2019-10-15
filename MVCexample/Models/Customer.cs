using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;//use While using Annotations
using System.ComponentModel.DataAnnotations.Schema;//for using Column

namespace MVCexample.Models
{
    public class Customer
    {
        public int ID { get; set; }


        [Required(ErrorMessage ="Name cannot be empty")]/*Modelstate step1*/
        [Column(TypeName="varchar")]
        [Display(Name = "Your Name")]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name ="Date of Birth")]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Column(TypeName ="char")]
        [StringLength(8)]
        public string Gender { get; set; }
        public long MobileNumber { get; set; }
        public string City { get; set; }




       // Reference Table
    public MembershipType MembershipType { get; set; }
        //Reference Column
        [Required]
        public int MembershipTypeId { get; set; }
    }
}