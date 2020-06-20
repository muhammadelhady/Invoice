using Invoice.Models;
using Invoice.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Invoice
{
    /// <summary>
    /// Interaction logic for InvoicesTables.xaml
    /// </summary>
    public partial class InvoicesTables : Window
    {
        private readonly DataService _dataService;

        public InvoicesTables()
        {
            _dataService = new DataService();
            InitializeComponent();
            FillData();
        }

        public async void FillData()
        {
            List < Invoices> invoices= await _dataService.GetAllInvoices();
            for (int i = 0; i < invoices.Count; i++)
                invoicesTableDatagrid.Items.Add(invoices[i]);


            List<InvoiceItems> invoiceItems = await _dataService.GetAllInvoicesItems();

            for (int i = 0; i < invoices.Count; i++)
                invoicesItemsTableDatagrid.Items.Add(invoiceItems[i]);
        }
    }
}
