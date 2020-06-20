using Invoice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Data
{
   public class DataContext : DbContext
    {
       
        public DbSet<Stores>  Stores { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Units>  Units { get; set; }
        public DbSet<Invoices> invoices { get; set; }
        public DbSet<InvoiceItems> invoiceItems { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL5053.site4now.net;Initial Catalog=DB_A6358C_muhammadelhadyy;User Id=DB_A6358C_muhammadelhadyy_admin;Password=fh1m03wfHima");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
