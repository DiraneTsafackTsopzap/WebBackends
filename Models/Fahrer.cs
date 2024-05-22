using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Fahrer
    {
        [Key]
        public Guid FahrerId { get; set; }

        public string FahrerName { get; set; }
        public string FahrerVorName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public string Image { get; set; }

       public Bus Bus { get; set; } 
        //public List<Bus> ListesBus { get; set; }
    }
}
