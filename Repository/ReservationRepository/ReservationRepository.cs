using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using webapp.DataContext;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Repository.ReservationRepository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly TicketReservationContext Ticket_Reservation_Context;
        private readonly INotyfService NotifyService;
        public ReservationRepository(TicketReservationContext ticketReservationContext, INotyfService toast)
        {
            this.Ticket_Reservation_Context = ticketReservationContext;
            this.NotifyService = toast;
        }


        public void ErstellungReservierung(ReservationViewModel reservation)
        {


            foreach (var seatId in reservation.SelectiertenSitzplatzen)
            {

                var reservationEntry = new Reservation
                {
                    SitzplatzId = seatId,
                    ReservationDate = reservation.ReservationDate,
                    IsReserved = true,
                    BenutzerId = Guid.Parse(reservation.UserId),
                    BusId = reservation.BusId
                };


                Ticket_Reservation_Context.reservation.Add(reservationEntry);



                this.NotifyService.Success("Erfolgreiche abbuchung");

            }

            Ticket_Reservation_Context.SaveChanges();

        }


        public Reservation ById(Guid id)
        {



            return (from obj in Ticket_Reservation_Context.reservation where obj.ReservationId == id select obj)
               .AsNoTracking().FirstOrDefault();
        }
















        public List<Reservation> GetALL()
        {
            return Ticket_Reservation_Context.reservation
        .Include(reservation => reservation.Bus)
        .ThenInclude(bus => bus.Fahrer)
        .Include(reservation => reservation.Benutzer)
        .Include(reservation => reservation.Sitzplatz)
        .ToList();

        }

        public List<Reservation> HoleReservierungenNachBenutzerId(Guid benutzerId)
        {
            return Ticket_Reservation_Context.reservation
                .Where(reservation => reservation.BenutzerId == benutzerId)
                .Include(bus => bus.Bus).ThenInclude(bus => bus.Fahrer)
                .Include(user => user.Benutzer)
                .Include(sieges => sieges.Sitzplatz)
                .ToList();

        }
        public void DeleteReservation(Guid reservationId, Guid benutzerId)
        {


            var reservation = Ticket_Reservation_Context.reservation.Find(reservationId);

            if (reservation != null)
            {
                if (reservation.BenutzerId == benutzerId)
                {
                    Ticket_Reservation_Context.reservation.Remove(reservation);
                    Ticket_Reservation_Context.SaveChanges();
                    this.NotifyService.Success("reservation erfolgreich gelöscht");
                }
            }
        }



        public List<Sitzplatz> VerfügbarePlätzeabrufen(Guid busId, DateTime date)
        {
            var reservierteSitzplatze = Ticket_Reservation_Context.reservation
                .Where(r => r.ReservationDate == date && r.BusId == busId)
                .Select(r => r.SitzplatzId)
                .ToList();

            var AlleSitzplatze = Ticket_Reservation_Context.sitzplatz
                .Where(s => s.BusId == busId)
                .ToList();

            var VerfugbareSitzplatze = AlleSitzplatze
                .Where(Sitzplatz => !reservierteSitzplatze.Contains(Sitzplatz.SitzplatzId))
                .ToList();


            return VerfugbareSitzplatze;



        }
    }
}
