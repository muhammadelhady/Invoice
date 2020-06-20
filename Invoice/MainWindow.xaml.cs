using Invoice.Data;
using Invoice.Helpers;
using Invoice.Models;
using Invoice.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Invoice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataService _dataService;
        private readonly DataContext _dataContext;
        private List<Stores> stores;
        private List<Items> items;
        private List<Units> units;
        private List<InvoiceItems> invoiceItems;

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.WindowState= WindowState.Maximized;
          
           
            _dataService = new DataService();
            _dataContext = new DataContext();
            invoiceItems = new List<InvoiceItems>();
            ClearAllFields();
            InvoiceDateTextbox.Text = System.DateTime.Now.ToString("MM/dd/yyyy");
            invoiceNumberTexbox.Text = new Random().Next(100, 9999999).ToString(); ;
            LoadStores();

          
        }



        public async void LoadStores()
        {
            stores =await _dataService.GetAllStores();
            foreach (Stores s in stores)
                storesDropdownList.Items.Add(s.StoreName);
            storesDropdownList.SelectedIndex = 0;
        }



        int GetSelectedStoreId(string Name)
        {
            foreach (Stores s in stores)
                if (s.StoreName == Name)
                    return s.StoreId;
            return 0;
        }


        int GetSelectedItemId(string Name)
        {
            foreach (Items i in items)
                if (i.ItemName == Name)
                    return i.ItemId;
            return 0;
        }

        int GetSelectedUnitId(string Name)
        {
            foreach (Units u in units)
                if (u.UnitName == Name)
                    return u.UnitId;
            return 0;
        }



        void UpdateInvoiceTotal(double Net)
        {
            double newTotal = double.Parse(invoiceTotalTextbox.Text) + Net;
            invoiceTotalTextbox.Text = newTotal.ToString();
            double newTaxes = newTotal * .14;
            invoiceTaxesTextbox.Text = newTaxes.ToString();
            double newNet = newTotal + newTaxes;
            invoiceNetTextbox.Text = newNet.ToString();
        }

        void ClearAllFields()
        {
            quantityTextbox.Text = "0";
            totalTextbox.Text = "0";
            discountTextbox.Text = "0";
            netTextbox.Text = "0";
        }
        private void ClearInvoiceFields()
        {
            invoiceTotalTextbox.Text = "0";
            invoiceTaxesTextbox.Text = "0";
            invoiceNetTextbox.Text = "0";
        }




        //buttons implemntaion 

        private void AddNewRowButton_Click(object sender, RoutedEventArgs e)
        {
            if (quantityTextbox.Text == "0" || unitsDropdownList.SelectedItem == null)
            {
                MessageBox.Show("please complete all fields ");
                return;
            }


            ItemCalculationHelper calculationHelper = new ItemCalculationHelper
            {
                Quantity = double.Parse(quantityTextbox.Text),
                Price = double.Parse(priceTextbox.Text),
                Discount = double.Parse(discountTextbox.Text)


            };

            calculationHelper.Total = calculationHelper.Quantity * calculationHelper.Price;
            calculationHelper.Net = calculationHelper.Total - calculationHelper.Discount;

            DataItemRow dataItemRow = new DataItemRow
            {
                Item = itemsDropdownList.Text,
                Unit = unitsDropdownList.Text,
                Price = priceTextbox.Text,
                Quantity = calculationHelper.Quantity.ToString(),
                Total = calculationHelper.Total.ToString(),
                Discount = calculationHelper.Discount.ToString(),
                Net = calculationHelper.Net.ToString()


            };

            dataGridview.Items.Add(dataItemRow);
            invoiceItems.Add(new InvoiceItems
            {
                ItemName = dataItemRow.Item,
                ItemUnit = dataItemRow.Unit,
                ItemPrice = double.Parse(dataItemRow.Price),
                ItemQuantity = long.Parse(dataItemRow.Quantity),
                ItemTotal = double.Parse(dataItemRow.Total),
                ItemDiscount = double.Parse(dataItemRow.Discount),
                ItemNet = double.Parse(dataItemRow.Net)

            });

            UpdateInvoiceTotal(calculationHelper.Net);
            ClearAllFields();
            storesDropdownList.IsEnabled = false;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (dataGridview.Items.Count==0)
            {
                MessageBox.Show("there is no items to save");
                return;
            }

            Invoices invoice = new Invoices
            {
                InvoiceDate = DateTime.Now ,
                InvoiceNumber = invoiceNumberTexbox.Text,
                StoreId = stores.Where(x=>x.StoreName== storesDropdownList.SelectedItem.ToString()).FirstOrDefault(),
                InvoiceTotal=double.Parse(invoiceTotalTextbox.Text),
                InvoiceTax=double.Parse(invoiceTaxesTextbox.Text),
                InvoiceNet=double.Parse(invoiceNetTextbox.Text)

            };

            for(int i = 0; i <invoiceItems.Count;i++)
            {
                invoiceItems[i].InvoiceId = invoice;
            }


            bool result = await _dataService.SaveInvoice(invoice, invoiceItems);

            if (result)
            {
                MessageBox.Show("data saved");
                dataGridview.Items.Clear();
                invoiceItems.Clear();
                ClearInvoiceFields();

            }
            else
            {
                MessageBox.Show("Error");

            }

        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFields();
            dataGridview.Items.Clear();
            invoiceItems.Clear();
            storesDropdownList.IsEnabled = true;
            ClearInvoiceFields();


        }



        //DropDown Lists Selection Change 
        private async void itemsDropdownList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (itemsDropdownList.SelectedItem == null)
                return;

            string selectedItem = itemsDropdownList.SelectedItem.ToString();
            int itemId = GetSelectedItemId(selectedItem);
            units = await _dataService.GetItemsUnits(itemId);
            unitsDropdownList.Items.Clear();
            foreach (Units u in units)
                unitsDropdownList.Items.Add(u.UnitName);
            unitsDropdownList.SelectedIndex = 0;

        }

        private async void storesDropdownList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (storesDropdownList.SelectedItem == null)
                return;
            string selectedStore = storesDropdownList.SelectedItem.ToString();
            int storeId = GetSelectedStoreId(selectedStore);
            items = await _dataService.GetStoreItems(storeId);
            itemsDropdownList.Items.Clear();
            foreach (Items i in items)
                itemsDropdownList.Items.Add(i.ItemName);
            itemsDropdownList.SelectedIndex = 0;

        }


        private void unitsDropdownList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (unitsDropdownList.SelectedItem == null)
                return;
            string selectedUnit = unitsDropdownList.SelectedItem.ToString();
            int unitId = GetSelectedUnitId(selectedUnit);
            Units u = units.FirstOrDefault(x => x.UnitId == unitId);
            priceTextbox.Text = u.UnitPrice.ToString();


        }


        //text Fields Change 
        private void quantityTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (quantityTextbox.Text == "")
                quantityTextbox.Text = "0";

            if (priceTextbox.Text == "")
                priceTextbox.Text = "0";

            ItemCalculationHelper calculationHelper = new ItemCalculationHelper
            {
                Quantity = double.Parse(quantityTextbox.Text),
                Price = double.Parse(priceTextbox.Text)

            };

            calculationHelper.Total = calculationHelper.Quantity * calculationHelper.Price;
            calculationHelper.Net = calculationHelper.Total - calculationHelper.Discount;

            totalTextbox.Text = calculationHelper.Total.ToString();

        }

        private void discountTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (totalTextbox.Text == "")
                totalTextbox.Text = "0";

            if (discountTextbox.Text == "")
                discountTextbox.Text = "0";

            netTextbox.Text = (double.Parse(totalTextbox.Text) - double.Parse(discountTextbox.Text)).ToString();
        }

        //validation on textbox inputs to only allow numbers 
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void showInvoicesTableButton_Click(object sender, RoutedEventArgs e)
        {
            InvoicesTables tables = new InvoicesTables();
            tables.Show();

        }
    }

   
}

  