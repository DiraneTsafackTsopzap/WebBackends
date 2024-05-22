using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Sitzplatz
    {
        [Key]
        public Guid SitzplatzId { get; set; }

        [Required(ErrorMessage = "wahlen Sie ein Bus aus ")]
        public Guid BusId { get; set; }

        [Required(ErrorMessage = "Die Sitznummer ist erforderlich.")]
        [Range(1, 10, ErrorMessage = "Die Sitznummer muss zwischen 1 und 10 liegen.")]

        public int Nummer { get; set; }

        public bool IsVerfugbar { get; set; }
        public Bus Bus { get; set; }


        
    }
}
