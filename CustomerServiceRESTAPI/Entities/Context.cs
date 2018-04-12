using System;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceRESTAPI.Entities
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
