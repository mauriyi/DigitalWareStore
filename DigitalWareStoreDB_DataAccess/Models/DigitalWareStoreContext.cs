using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DigitalWareStoreDB_DataAccess.Models
{
    public partial class DigitalWareStoreContext : DbContext
    {
        public DigitalWareStoreContext()
        {
        }

        public DigitalWareStoreContext(DbContextOptions<DigitalWareStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("Client");

                entity.Property(e => e.IdClient).HasColumnName("id_client");

                entity.Property(e => e.Address)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.IdInvoice);

                entity.ToTable("Invoice");

                entity.Property(e => e.IdInvoice).HasColumnName("id_invoice");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.IdClient).HasColumnName("id_client");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_Invoice");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(e => e.IdInvoiceItem);

                entity.ToTable("Invoice_items");

                entity.Property(e => e.IdInvoiceItem).HasColumnName("id_invoice_item");

                entity.Property(e => e.IdInvoice).HasColumnName("id_invoice");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total_price");

                entity.Property(e => e.Units).HasColumnName("units");

                entity.HasOne(d => d.IdInvoiceNavigation)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.IdInvoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Items_Invoice_Invoice");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Items_Invoice");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("Product");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Nombre).HasColumnName("nombre");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.Stock).HasColumnName("stock");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
