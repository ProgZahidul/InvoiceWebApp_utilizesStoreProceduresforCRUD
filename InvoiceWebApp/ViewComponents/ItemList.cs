using InvoiceWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWebApp.ViewComponents
{
    public class ItemList : ViewComponent
    {

        public IViewComponentResult Invoke(List<TicketInfo> data)
        {

            ViewBag.Count = data.Count;
            ViewBag.Total = data.Sum(i => i.Total);

            return View(data);
        }



    }
}
