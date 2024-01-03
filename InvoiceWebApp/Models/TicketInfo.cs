using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace InvoiceWebApp.Models
{
    public class TicketInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketInfoId { get; set; }
        [Required]
        public int? TicketId { get; set; }

        public int Quantity { get; set; }

        public decimal Total => this.ticket is null ? 0 : this.ticket.Price * this.Quantity;


        public int? InvoiceId { get; set; }

        public Ticket? ticket { get; set; }
        public Invoice? Invoice { get; set; }

    }
}
