
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;
using SimpleHashing.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.DataContext;
using webapp.Models;

namespace webapp.Repository.BenuterRepository
{
    public class BenutzerRepository : IBenutzerRepository
    {
        private readonly TicketReservationContext Ticket_Reservation_Context; 
        private ISimpleHash SimpleHash = new SimpleHash();
        private readonly INotyfService NotifyService;

        public BenutzerRepository(TicketReservationContext _TicketReservationContext , INotyfService Toast)
        {
            this.Ticket_Reservation_Context = _TicketReservationContext;
            this.NotifyService = Toast;
        }

        public Benutzer ById(Guid id)
        {


            if (id == Guid.Parse("7B35F2B4-BA92-4990-B2F5-7D0BE1423D6C"))
            {
                return new Benutzer
                {
                    Firstname = "Dirane Tsafack",
                    Lastname = "Tsopzap",
                    Email = "admin@gmail.com",
                    IsAdmin = true,
                    Password = "admin",
                    BenutzerId = Guid.Parse("7B35F2B4-BA92-4990-B2F5-7D0BE1423D6C")
                };
            }


            return (from obj in Ticket_Reservation_Context.benutzer where obj.BenutzerId == id select obj)
               .AsNoTracking().FirstOrDefault();
        }

        public void Delete(Guid id)
        {
            var obj = Ticket_Reservation_Context.benutzer.Find(id);
            if (obj != null)
            {
                Ticket_Reservation_Context.benutzer.Remove(obj);
               
                Ticket_Reservation_Context.SaveChanges();
                this.NotifyService.Success("Konto erfolgreich gelöscht");
            }
        }

        public Benutzer FindByEmailAndPassword(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
               
                return null;
            }

            if (email.Equals("admin@gmail.com", StringComparison.OrdinalIgnoreCase) && password.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                // Créer et retourner l'utilisateur fixe
                return new Benutzer
                {
                    Firstname = "Dirane Tsafack",
                    Lastname = "Tsopzap",
                    Email = "admin@gmail.com",
                    IsAdmin = true,
                    Password = "admin",
                    BenutzerId = Guid.Parse("7B35F2B4-BA92-4990-B2F5-7D0BE1423D6C")
                };
            }




            var list = (from obj in Ticket_Reservation_Context.benutzer
                        where obj.Email.Equals(email)
                        select obj).ToList();



            foreach (var obj in list)
            {
                if (SimpleHash.Verify(password, obj.Password))
                {
                    return obj;
                }
            }

            return null;
          
        }

        public List<Benutzer> GetAll(int from = 0, int max = 1000)
        {
            return Ticket_Reservation_Context.benutzer.AsNoTracking().Skip(from).Take(max).ToList();
        }

        public void Save(Benutzer benutzer)
        {
            string hashpassword = SimpleHash.Compute(benutzer.Password);
            benutzer.Password = hashpassword;
            //benutzer.IsAdmin = true;

            Ticket_Reservation_Context.Add(benutzer);
            Ticket_Reservation_Context.SaveChanges();
            this.NotifyService.Success("Konto erfolgreich erstellt ");


        }


        public void Update(Benutzer benutzer)
        {
            
                Ticket_Reservation_Context.benutzer.Attach(benutzer);
                Ticket_Reservation_Context.Entry(benutzer).State = EntityState.Modified;
                Ticket_Reservation_Context.SaveChanges();
                Ticket_Reservation_Context.Entry(benutzer).State = EntityState.Detached;
                this.NotifyService.Success("Konto erfolgreich verändert");

        }
    }
}
