using Invoice.Data;
using Invoice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Services
{
    class DataService : IdataService
    {
        private readonly DataContext _context;
      

        public DataService()
        {
            _context = new DataContext();
        
        }

        public async Task<List<Stores>> GetAllStores()
        {
            List<Stores> list= await _context.Stores.ToListAsync();

            return list;
        }

        public async Task<List<Items>> GetStoreItems(int storeId)
        {
            List<Items> list = await _context.Items.Where(x=>x.Stores.StoreId==storeId).ToListAsync();

            return list;
        }

        public async Task<List<Units>> GetItemsUnits(int itemId)
        {
            List<Units> list = await _context.Units.Where(x=> x.Items.ItemId==itemId).ToListAsync();

            return list;
        }

        public async Task<bool> SaveInvoice(Invoices invoice, List<InvoiceItems> invoiceItems)
        {
            await _context.invoices.AddAsync(invoice);
            await _context.invoiceItems.AddRangeAsync(invoiceItems);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Invoices>> GetAllInvoices()
        {
            return await _context.invoices.ToListAsync();
        }


        public async Task<List<InvoiceItems>> GetAllInvoicesItems()
        {
            return await _context.invoiceItems.ToListAsync();
        }

        

     
    }
}
