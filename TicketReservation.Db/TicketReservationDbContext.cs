using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TicketReservation.Domain;

namespace TicketReservation.Db
{
    public class TicketReservationDbContext:DbContext
    {
        public TicketReservationDbContext(DbContextOptions<TicketReservationDbContext> options) : base(options)
        {

        }

        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<TicketReservation.Domain.TicketReservation> TicketReservation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, Name = "Name 1" },
                new Reservation { Id = 2, Name = "Name 2" },
                new Reservation { Id = 3, Name = "Name 3" }
            );
        }
    }
}
