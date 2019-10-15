using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCexample.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required(ErrorMessage="MovieName cannot be left Blank" )]
        
        public string MovieName { get; set; }
       // public string Genre { get; set; }
       [Required]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage ="DateAdded cannot be Empty")]
        public DateTime DateAdded { get; set; }

        [Required]
        public int? AvailableStock{ get; set; }

        //Reference table
        public Genre Genre { get; set; }
        //reference column
        public int GenreId{ get; set; }


    }
}