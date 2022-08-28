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

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int IdProduct { get; set; }
        [Required]
        public int Nombre { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
