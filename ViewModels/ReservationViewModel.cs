using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.ViewModels
{
    public class ReservationViewModel
    {
        [Required]
        public Guid BusId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        public List<Guid> SelectiertenSitzplatzen { get; set; }
    }
}
