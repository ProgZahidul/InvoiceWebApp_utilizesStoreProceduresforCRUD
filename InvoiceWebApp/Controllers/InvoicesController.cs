using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceWebApp.Models;

namespace InvoiceWebApp.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoiceContext _context;
        private readonly IWebHostEnvironment _enc;

        public InvoicesController(InvoiceContext context, IWebHostEnvironment enc)
        {
            _context = context;
            _enc = enc;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var data = await _context.Invoices.Include(i => i.TicketInfos).ThenInclude(p => p.ticket).ToListAsync();


            ViewBag.Count = data.Count;
            ViewBag.GrandTotal = data.Sum(i => i.TicketInfos.Sum(i => i.Total));

            ViewBag.Average = data.Count > 0 ? data.Average(i => i.TicketInfos.Sum(i => i.Total)) : 0;
            return View(data);
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.Include(i => i.TicketInfos).ThenInclude(p => p.ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            return View(new Invoice());
        }

        // POST: Invoices/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,CreatedDate,PassengerName,Address,ContactNo,CardHolder,ImagePath,ImageFile,TicketInfos")] Invoice invoice, string command = "")
        {
            if (invoice.ImageFile != null)
            {
                invoice.ImagePath = "\\Image\\" + invoice.ImageFile.FileName;
                string servaerPath = _enc.WebRootPath + invoice.ImagePath;
                using FileStream stream = new FileStream(servaerPath, FileMode.Create);
                await invoice.ImageFile.CopyToAsync(stream);

                TempData["Image"] = invoice.ImagePath;

            }
            else
            {
                invoice.ImagePath = TempData["Image"]?.ToString();
            }
            if (command == "Add")
            {
                invoice.TicketInfos.Add(new());
                return View(invoice);
            }
            else if (command.Contains("delete"))// delete-3-sdsd-5   ["delete", "3"]
            {
                int idx = int.Parse(command.Split('-')[1]);

                invoice.TicketInfos.RemoveAt(idx);
                ModelState.Clear();
                return View(invoice);
            }

            if (ModelState.IsValid)
            {

                var rows = await _context.Database.ExecuteSqlRawAsync("exec SpInsertInvoice @p0, @p1, @p2, @p3, @p4, @p5", invoice.CreatedDate, invoice.PassengerName, invoice.Address, invoice.ContactNo, invoice.ImagePath, invoice.CardHolder);


                if (rows > 0)
                {
                    invoice.Id = _context.Invoices.Max(x => x.Id);

                    foreach (var item in invoice.TicketInfos)
                    {
                        await _context.Database.ExecuteSqlRawAsync("exec SpInsertTicketInfo @p0, @p1, @p2", invoice.Id, item.TicketId, item.Quantity);
                    }
                }

                return RedirectToAction(nameof(Index));

            }
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.Include(i => i.TicketInfos).ThenInclude(p => p.ticket).FirstOrDefaultAsync(i => i.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDate,PassengerName,Address,ContactNo,CardHolder,ImagePath,ImageFile,TicketInfos")] Invoice invoice, string command = "")
        {
            if (invoice.ImageFile != null)
            {
                invoice.ImagePath = "\\Image\\" + invoice.ImageFile.FileName;
                string servaerPath = _enc.WebRootPath + invoice.ImagePath;
                using FileStream stream = new FileStream(servaerPath, FileMode.Create);
                await invoice.ImageFile.CopyToAsync(stream);


                TempData["Image"] = invoice.ImagePath;

            }
            else
            {
                invoice.ImagePath = TempData["Image"]?.ToString();
            }
            if (command == "Add")
            {
                invoice.TicketInfos.Add(new());
                return View(invoice);
            }
            else if (command.Contains("delete"))// delete-3   ["delete", "3"]
            {
                int idx = int.Parse(command.Split('-')[1]);

                invoice.TicketInfos.RemoveAt(idx);
                ModelState.Clear();
                return View(invoice);
            }
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var row = await _context.Database.ExecuteSqlRawAsync("exec SpUpdateInvoice @p0, @p1, @p2, @p3, @p4, @p5,@p6", invoice.Id, invoice.CreatedDate, invoice.PassengerName, invoice.Address, invoice.ContactNo, invoice.ImagePath, invoice.CardHolder);


                    foreach (var ticketInfo in invoice.TicketInfos)
                    {
                        await _context.Database.ExecuteSqlRawAsync("exec SpInsertTicketInfo @p0, @p1, @p2", invoice.Id, ticketInfo.TicketId, ticketInfo.Quantity);
                    }

                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.Include(i => i.TicketInfos).ThenInclude(p => p.ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Database.ExecuteSqlAsync($"exec SpDeleteInvoice {id}");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        [Route("~/deleteinvoice/{id}")]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            await _context.Database.ExecuteSqlAsync($"exec spDeleteInvoice {id}");

            return Ok();
        }
        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
