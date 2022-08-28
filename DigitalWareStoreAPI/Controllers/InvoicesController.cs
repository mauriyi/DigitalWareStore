using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DigitalWareStoreDB_DataAccess.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DigitalWareStoreAPI.Controllers
{
    [Route("DigitalWareStoreAPI/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly DigitalWareStoreContext _context;

        public InvoicesController(DigitalWareStoreContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
          if (_context.Invoices == null)
          {
              return NotFound();
          }

            var invoices = await _context.Invoices.ToListAsync();

            foreach (var invoice in invoices)
            {
                var items = _context.InvoiceItems.Where(x => x.IdInvoice == invoice.IdInvoice).ToList();
                invoice.InvoiceItems = items;
            }

            return invoices;
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            var items = _context.InvoiceItems.Where(x => x.IdInvoice == invoice.IdInvoice).ToList();
            invoice.InvoiceItems = items;
            return invoice;
        }

        // PUT: api/Invoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            if (id != invoice.IdInvoice)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Invoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
          if (_context.Invoices == null)
          {
              return Problem("Error Invoice is null.");
          }

            var client = _context.Clients.Where(x => x.IdClient == invoice.IdClient).FirstOrDefault();

            if(client == default)
            {
                return Conflict(new { message = $"No existe cliente Id: {invoice.IdClient}." });
            }

            var products = _context.Products.Where(x => x.Stock > 0).ToList();
            foreach(var item in invoice.InvoiceItems)
            {
                var product = products.Where(x => x.IdProduct == item.IdProduct).FirstOrDefault();
                if (product != default)
                {
                    if (product.Stock >= item.Units)
                    {
                        products.Where(x => x.IdProduct == item.IdProduct).First().Stock -= item.Units;
                    }
                    else
                    {
                        return Conflict(new { message = $"Producto Id: {item.IdProduct} - {product.Nombre}, no cuenta con Stock Suficiente." });
                    }
                }
                else
                {
                    return Conflict(new { message = $"No existe el Producto Id: {item.IdProduct}." });
                }
            }

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.IdInvoice }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            var items = await _context.InvoiceItems.Where(x => x.IdInvoice == invoice.IdInvoice).ToListAsync();
            items.ForEach(x => _context.InvoiceItems.Remove(x));

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(int id)
        {
            return (_context.Invoices?.Any(e => e.IdInvoice == id)).GetValueOrDefault();
        }
    }
}
