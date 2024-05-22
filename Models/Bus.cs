using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Bus
    {
        [Key]

        public Guid BusId { get; set; }


        [Required(ErrorMessage = "Image ist erforderlich.")]
        [RegularExpression(@"^https?://.+\.(jpg|jpeg|png|gif)$", ErrorMessage = "Url Image ist nicht gültig")]
        public string Image { get; set; }


        [Required(ErrorMessage = "Der StartZiel ist erforderlich.")]
        public string StartZiel { get; set; }

        [Required(ErrorMessage = "Der EndZiel ist erforderlich.")]
        public string EndZiel { get; set; }


        [Required(ErrorMessage = "Der BusName ist erforderlich.")]
        public string BusName { get; set; }

        [Required(ErrorMessage = "Die AbfahrtsZeit ist erforderlich.")]
     
        public DateTime AbfahrtsZeit { get; set; }

       
        public bool KlimaAnlage { get; set; }

        public bool Unterhaltung { get; set; } // Hier wird Unterhaltungsausstattung gemeint


        public Fahrer Fahrer { get; set; }
        public Guid FahrerId { get; set; }


        [Required(ErrorMessage = "Bitte Wahlen Sie ein BusCategory aus ")]


        public BusCategory BusCategory { get; set; }
    }


    public enum BusCategory
    {
        StandardBus = 1, LuxusBus = 2, SchlafBus = 3, Vip_Bus = 4
    }
}
