using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int YearOfBirth { get; set; }
        public int Age { get; set; }
        public DateTime CreationDate { get; set; }

        // Foreign Key
        public int SpeciesId { get; set; }
        // Navigation property
        public Species Species { get; set; }
    }
}