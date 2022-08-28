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

        [Key]
        public int IdInvoice { get; set; }
        public int IdClient { get; set; }
        public DateTime Date { get; set; }

        [JsonIgnore]
        public virtual Client? IdClientNavigation { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
