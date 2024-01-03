using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InvoiceWebApp.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public string PassengerName { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        [Display(Name = "Card Holder")]
        public bool CardHolder { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public List<TicketInfo> TicketInfos { get; set; } = new List<TicketInfo>();
    }
}
