using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Reservation
    {
        [Key]
        public Guid ReservationId { get; set; }

        [Required]
        public Guid SitzplatzId { get; set; }
        public Sitzplatz Sitzplatz { get; set; }


        public Bus Bus { get; set; }

        public Guid BenutzerId { get; set; }
        public Benutzer Benutzer { get; set; }

        [Required]
        public Guid BusId { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        public bool IsReserved { get; set; }




       



    }
}
