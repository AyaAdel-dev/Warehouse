using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EntityProject
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Entry_document> Entry_document { get; set; }
        public virtual DbSet<Entry_Product> Entry_Product { get; set; }
        public virtual DbSet<Exchange_document> Exchange_document { get; set; }
        public virtual DbSet<Exchange_product> Exchange_product { get; set; }
        public virtual DbSet<Existing_products> Existing_products { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<transaction_log> transaction_log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.fax)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.mail)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.website)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Exchange_document)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.customer_name);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.transaction_log)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.customer_name);

            modelBuilder.Entity<Entry_document>()
                .Property(e => e.store_name)
                .IsUnicode(false);

            modelBuilder.Entity<Entry_document>()
                .Property(e => e.supplier_name)
                .IsUnicode(false);

            modelBuilder.Entity<Entry_document>()
                .HasMany(e => e.Entry_Product)
                .WithRequired(e => e.Entry_document)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exchange_document>()
                .Property(e => e.store_name)
                .IsUnicode(false);

            modelBuilder.Entity<Exchange_document>()
                .Property(e => e.customer_name)
                .IsUnicode(false);

            modelBuilder.Entity<Exchange_document>()
                .HasMany(e => e.Exchange_product)
                .WithRequired(e => e.Exchange_document)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exchange_product>()
                .Property(e => e.supplier_name)
                .IsUnicode(false);

            modelBuilder.Entity<Existing_products>()
                .Property(e => e.store_name)
                .IsUnicode(false);

            modelBuilder.Entity<Existing_products>()
                .Property(e => e.supplier_name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.unit)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Entry_Product)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.Product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Exchange_product)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.Product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Existing_products)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.transaction_log)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<Store>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.manager)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Entry_document)
                .WithOptional(e => e.Store)
                .HasForeignKey(e => e.store_name);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Exchange_document)
                .WithOptional(e => e.Store)
                .HasForeignKey(e => e.store_name);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Existing_products)
                .WithOptional(e => e.Store)
                .HasForeignKey(e => e.store_name);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.transaction_log)
                .WithOptional(e => e.Store)
                .HasForeignKey(e => e.store_name);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.transaction_log1)
                .WithOptional(e => e.Store1)
                .HasForeignKey(e => e.new_store_name);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.fax)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.mail)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.website)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Entry_document)
                .WithOptional(e => e.Supplier)
                .HasForeignKey(e => e.supplier_name);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Exchange_product)
                .WithOptional(e => e.Supplier)
                .HasForeignKey(e => e.supplier_name);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Existing_products)
                .WithOptional(e => e.Supplier)
                .HasForeignKey(e => e.supplier_name);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.transaction_log)
                .WithOptional(e => e.Supplier)
                .HasForeignKey(e => e.supplier_name);

            modelBuilder.Entity<transaction_log>()
                .Property(e => e.transaction_label)
                .IsUnicode(false);

            modelBuilder.Entity<transaction_log>()
                .Property(e => e.store_name)
                .IsUnicode(false);

            modelBuilder.Entity<transaction_log>()
                .Property(e => e.new_store_name)
                .IsUnicode(false);

            modelBuilder.Entity<transaction_log>()
                .Property(e => e.supplier_name)
                .IsUnicode(false);

            modelBuilder.Entity<transaction_log>()
                .Property(e => e.customer_name)
                .IsUnicode(false);
        }
    }
}
