using Invoice.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Services
{
    interface IdataService
    {

        public  Task<List<Stores>> GetAllStores();

        public  Task<List<Items>> GetStoreItems(int storeId);

        public  Task<List<Units>> GetItemsUnits(int itemId);

        public Task<bool> SaveInvoice(Invoices invoice, List<InvoiceItems> invoiceItems);

        public Task<List<Invoices>> GetAllInvoices();



        public Task<List<InvoiceItems>> GetAllInvoicesItems();
       

    }
}
