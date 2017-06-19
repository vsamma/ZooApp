using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class AnimalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfBirth { get; set; }
        public int Age { get; set; }
        public DateTime CreationDate { get; set; }
        public string SpeciesName { get; set; }
    }
}