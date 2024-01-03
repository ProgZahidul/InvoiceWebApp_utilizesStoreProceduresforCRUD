using Microsoft.EntityFrameworkCore;

namespace InvoiceWebApp.Models
{
    public class InvoiceContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<TicketInfo> TicketInfos { get; set; }

        public InvoiceContext(DbContextOptions opt) : base(opt)
        {

        }
    }
}
