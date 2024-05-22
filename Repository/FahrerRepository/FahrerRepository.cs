using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.DataContext;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Repository.FahrerRepository
{
    public class FahrerRepository : IFahrerRepository
    {
        private readonly TicketReservationContext Ticket_Reservation_Context; 
     
        private readonly INotyfService NotifyService;


        public FahrerRepository( TicketReservationContext ticketReservationContext , INotyfService Toastnotifications)
        {
            this.Ticket_Reservation_Context = ticketReservationContext;
            this.NotifyService = Toastnotifications;
        }


      
        public Fahrer ById(Guid id)
        {
            return (from obj in Ticket_Reservation_Context.fahrer where obj.FahrerId == id select obj)
            .AsNoTracking().FirstOrDefault();

           // return Ticket_Reservation_Context.fahrertable.AsNoTracking().FirstOrDefault(n => n.FahrerId == id);
           


        }


        public void Delete(Guid id)
        {
            var obj = Ticket_Reservation_Context.fahrer.Find(id);
            if (obj != null)
            {
                Ticket_Reservation_Context.fahrer.Remove(obj);

                Ticket_Reservation_Context.SaveChanges();
                this.NotifyService.Success("Fahrer erfolgreich gelöscht");
            }
        }

        public List<Fahrer> GetAll(int from = 0, int max = 1000)
        {
            return Ticket_Reservation_Context.fahrer.AsNoTracking().Skip(from).Take(max).ToList();
        }

        public void Save(FahrerViewModel Neuer_Fahrer)
        {
            var Fahrer = new Fahrer
            {
                FahrerName = Neuer_Fahrer.FahrerName,
                FahrerVorName = Neuer_Fahrer.FahrerVorName,
                Age = Neuer_Fahrer.Age,
                Email = Neuer_Fahrer.Email,
                Image = Neuer_Fahrer.Image,

            };


            Ticket_Reservation_Context.fahrer.Add(Fahrer);
            Ticket_Reservation_Context.SaveChanges();
            this.NotifyService.Success("Fahrer erfolgreich Erstellt ");
        }

        public Fahrer Update(Guid id, FahrerViewModel fahrer)
        {
            var existingBenutzer = Ticket_Reservation_Context.fahrer.AsNoTracking().FirstOrDefault(n => n.FahrerId == id);

            if (existingBenutzer != null)
            {
                
                existingBenutzer.FahrerName = fahrer.FahrerName;
                existingBenutzer.FahrerVorName = fahrer.FahrerVorName;
                existingBenutzer.Age = fahrer.Age;
                existingBenutzer.Image = fahrer.Image;
                existingBenutzer.Email = fahrer.Email;


                Ticket_Reservation_Context.Update(existingBenutzer);
                Ticket_Reservation_Context.SaveChanges();
                this.NotifyService.Success("Fahrer erfolgreich geändert ");
            }

            return existingBenutzer;
        }

       
    }
}
