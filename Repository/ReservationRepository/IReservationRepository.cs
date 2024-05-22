using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Repository.ReservationRepository
{
    public interface IReservationRepository
    {
        void ErstellungReservierung(ReservationViewModel reservation);
    
        void DeleteReservation(Guid reservationId, Guid benutzerId);

        List<Reservation> GetALL();

         Reservation ById(Guid id);


        List<Reservation> HoleReservierungenNachBenutzerId(Guid benutzerId);
        List<Sitzplatz> VerfügbarePlätzeabrufen(Guid busId, DateTime date);
    }
}
