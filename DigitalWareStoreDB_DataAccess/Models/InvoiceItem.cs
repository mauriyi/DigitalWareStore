using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace DigitalWareStoreDB_DataAccess.Models
{
    public partial class InvoiceItem
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int IdInvoiceItem { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int IdInvoice { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int IdProduct { get; set; }
        [Required]
        public int Units { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }

        public virtual Invoice IdInvoiceNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
