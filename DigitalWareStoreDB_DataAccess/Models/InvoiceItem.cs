using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DigitalWareStoreDB_DataAccess.Models
{
    public partial class InvoiceItem
    {
        public int IdInvoiceItem { get; set; }
        public int IdInvoice { get; set; }
        public int IdProduct { get; set; }
        public int Units { get; set; }
        public decimal TotalPrice { get; set; }

        [JsonIgnore]
        public virtual Invoice? IdInvoiceNavigation { get; set; }
        [JsonIgnore]
        public virtual Product? IdProductNavigation { get; set; }
    }
}
