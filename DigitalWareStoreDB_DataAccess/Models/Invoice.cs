using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DigitalWareStoreDB_DataAccess.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int IdInvoice { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int IdClient { get; set; }
        [Required]
        public DateTime Date { get; set; }
       
        public virtual Client IdClientNavigation { get; set; } = null!;
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
