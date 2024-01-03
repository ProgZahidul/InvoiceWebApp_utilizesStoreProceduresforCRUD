namespace InvoiceWebApp.Models
{
    public class Ticket
    {

        public int TicketId { get; set; }
        public string RouteName { get; set; }

        public decimal Price { get; set; }

        public IList<TicketInfo> TicketInfos { get; set; } = new List<TicketInfo>();


    }
}
