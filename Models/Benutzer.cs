using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Benutzer
    {

        [Key]

        public Guid BenutzerId { get; set; }



        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        public bool IsAdmin { get; set; }

        public ICollection<Reservation> ListesReservations { get; set; }

    }


}
