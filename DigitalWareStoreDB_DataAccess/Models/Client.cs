using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DigitalWareStoreDB_DataAccess.Models
{
    public partial class Client
    {
        public Client()
        {
            Invoices = new HashSet<Invoice>();
        }

        [Key]
        public int IdClient { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
