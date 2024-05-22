using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.DataContext;
using webapp.Models;

namespace webapp.Repository.BusRepository
{
    public class BusRepository : IBusRepository
    {

        private readonly TicketReservationContext Ticket_Reservation_Context;
        private readonly INotyfService NotifyService;
        public BusRepository(TicketReservationContext _TicketReservationContext , INotyfService Toasts)
        {
            this.Ticket_Reservation_Context = _TicketReservationContext;
            NotifyService = Toasts;
        }
        public List<Bus> GetAll(int from = 0, int max = 1000)
        {
            // Utilisez Include pour charger les données liées Fahrer 
            return Ticket_Reservation_Context.bus
                .Include(bus => bus.Fahrer)
                .AsNoTracking()
                .Skip(from)
                .Take(max)
                .ToList();
        }


        public void Save(Bus bus)
        {
           
                Ticket_Reservation_Context.bus.Add(bus);
                Ticket_Reservation_Context.SaveChanges();
                this.NotifyService.Success("Bus erfolgreich Erstellt");
          
             
          

        }

        public void Delete(Guid id)
        {
           var obj = Ticket_Reservation_Context.bus.Find(id);
            
            if (obj != null)
            {
                Ticket_Reservation_Context.bus.Remove(obj);

                Ticket_Reservation_Context.SaveChanges();
                this.NotifyService.Success("Konto erfolgreich gelöscht");
            }
        }

        public Bus ById(Guid id)
        {
            return (from obj in Ticket_Reservation_Context.bus where obj.BusId == id select obj)
               .AsNoTracking().FirstOrDefault();
        }
    }
}
