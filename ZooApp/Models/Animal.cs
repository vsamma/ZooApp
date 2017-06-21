using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class Animal
    {
        //[Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Year of Birth is required")]
        public int YearOfBirth { get; set; }
        //[Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        //[Required(ErrorMessage = "Creation date is required")]
        public DateTime? CreationDate { get; set; }

        // Foreign Key
        //[Required(ErrorMessage = "Species Id is required")]
        public int SpeciesId { get; set; }
        // Navigation property
        public Species Species { get; set; }
    }
}