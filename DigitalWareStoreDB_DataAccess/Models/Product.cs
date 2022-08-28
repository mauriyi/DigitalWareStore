using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DigitalWareStoreDB_DataAccess.Models
{
    public partial class Product
    {
        public Product()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        [Key]
        public int IdProduct { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        [JsonIgnore]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
