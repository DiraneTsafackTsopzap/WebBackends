using Microsoft.EntityFrameworkCore;

using webapp.Models;

namespace webapp.DataContext
{
    public class TicketReservationContext : DbContext
    {
        public TicketReservationContext(DbContextOptions<TicketReservationContext> options) : base(options)
        {

        }


        public DbSet<Benutzer> benutzer { get; set; }
        public DbSet<Fahrer> fahrer { get; set; }
        public DbSet<Bus> bus { get; set; }
        public DbSet<Sitzplatz> sitzplatz { get; set; }

        public DbSet<Reservation> reservation { get; set; }



        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            // modelbuilder.Entity<Benutzer>();

            modelbuilder.Entity<Benutzer>()
           .HasMany(u => u.ListesReservations)



           .WithOne(r => r.Benutzer)
           .HasForeignKey(r => r.BenutzerId);



            modelbuilder.Entity<Fahrer>();
            modelbuilder.Entity<Bus>();
            modelbuilder.Entity<Sitzplatz>();
            modelbuilder.Entity<Reservation>();




            modelbuilder.Entity<Fahrer>()
                .HasOne(d => d.Bus)

                .WithOne(b => b.Fahrer)

                .HasForeignKey<Bus>(b => b.FahrerId);



        }
    }


}
