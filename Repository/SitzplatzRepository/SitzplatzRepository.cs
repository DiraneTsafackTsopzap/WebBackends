using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.DataContext;
using webapp.Models;

namespace webapp.Repository.SitzplatzRepository
{
    public class SitzplatzRepository : ISitzplatzRepository
    {
        private readonly TicketReservationContext Ticket_Reservation_Context;
        private readonly INotyfService NotifyService;
        public SitzplatzRepository(TicketReservationContext _TicketReservationContext, INotyfService Toasts)
        {
            this.Ticket_Reservation_Context = _TicketReservationContext;
            NotifyService = Toasts;
        }

        public void Add(Sitzplatz sitzplatz)
        {
         
            Sitzplatz existierendesSitzplatz = Ticket_Reservation_Context.sitzplatz.FirstOrDefault(s =>
                s.Nummer == sitzplatz.Nummer && s.BusId == sitzplatz.BusId);

            if (existierendesSitzplatz == null)
            {
                Ticket_Reservation_Context.sitzplatz.Add(sitzplatz);
                Ticket_Reservation_Context.SaveChanges();
                this.NotifyService.Success("Erfolgreiche Hinzufügung");
            }
            else
            {
                this.NotifyService.Warning("Dieser Sitzplatz " + sitzplatz.Nummer + " existiert bereits für diesen Bus.");
            }
        }


        public void Delete(Guid id)
        {
            var obj = Ticket_Reservation_Context.sitzplatz.Find(id);

            if (obj != null)
            {
                Ticket_Reservation_Context.sitzplatz.Remove(obj);

                Ticket_Reservation_Context.SaveChanges();
                this.NotifyService.Success("Sitzplatz erfolgreich gelöscht");
            }
        }

       

        public Sitzplatz GetById(Guid id)
        {
            return (from obj in Ticket_Reservation_Context.sitzplatz where obj.SitzplatzId == id select obj)
               .AsNoTracking().FirstOrDefault();
        }

        
        public List<Sitzplatz> GetAll(int from = 0, int max = 1000)
        {
            return Ticket_Reservation_Context.sitzplatz
                .Include(bus => bus.Bus)
                .AsNoTracking()
                .OrderBy(sitzplatz => sitzplatz.Nummer)  // Tri par ordre croissant de sitzplatznummer
                .Skip(from)
                .Take(max)
                .ToList();
        }

    }
}
