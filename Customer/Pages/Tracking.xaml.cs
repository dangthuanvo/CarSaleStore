using CarSalesSystem.Admin.Pages;
using CarSalesSystem.Customer.Windows;
using CarSalesSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace CarSalesSystem.Customer.Pages
{
    /// <summary>
    /// Interaction logic for Bill.xaml
    /// </summary>
    /// 
    //class arrOrderBill
    //{
    //    public string OB_ID { get; set; }
    //    public Nullable<System.DateTime> OB_DATEB { get; set; }
    //    public string CUSTOMER_ID { get; set; }
    //    public string PRO_ID { get; set; }
    //    public Nullable<int> QUANTITY { get; set; }
    //    public string RANK_ID { get; set; }
    //    public Nullable<decimal> TOTAL_PRICE { get; set; }
    //    public string PAID { get; set; }

    //    public CUSTOMER CUSTOMER { get; set; }
    //    public PRODUCT PRODUCT { get; set; }
    //    public RANK_MONEY RANK_MONEY { get; set; }
    //}
    public partial class Tracking : Page
    {
        //ObservableCollection<ORDERBILL> orderBills;
        CUSTOMER cus;
        public Tracking(CUSTOMER _cus)
        {
            InitializeComponent();
            cus = _cus;
            List<MAINTENANCEBILL> list = DataProvider.Ins.DB.MAINTENANCEBILLs.Where(x => x.CUSTOMER_ID == cus.CUS_ID).ToList();
            //MessageBox.Show(list.Count.ToString());
            datagridOrderBill.ItemsSource = list;

        }
    

            private void datagridOrderBill_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ////MessageBox.Show("click");
            //arrOrderBill tmp = datagridOrderBill.SelectedItem as arrOrderBill;
            //ORDERBILL ordBill = new ORDERBILL();
            //ordBill.OB_ID = tmp.OB_ID;
            //ordBill.OB_DATEB = tmp.OB_DATEB;
            //ordBill.CUSTOMER_ID = tmp.CUSTOMER_ID;
            //ordBill.PRO_ID = tmp.PRO_ID;
            //ordBill.QUANTITY = tmp.QUANTITY;
            //ordBill.RANK_ID = tmp.RANK_ID;
            //ordBill.TOTAL_PRICE = tmp.TOTAL_PRICE;
            //ordBill.CUSTOMER = tmp.CUSTOMER;
            //ordBill.PRODUCT = tmp.PRODUCT;
            //ordBill.RANK_MONEY = tmp.RANK_MONEY;
            //if (tmp.PAID == "Paid")
            //{
            //    ordBill.PAID = 1;
            //}
            //else
            //    ordBill.PAID = 0;
            //if (ordBill != null)
            //{
            //    MaintenanceBill maintainBillWindow = new MaintenanceBill(cus,ordBill);
            //    maintainBillWindow.Show();
            //}
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txb = sender as TextBox;
            if (txb.Text != "")
            {
                var filterList = DataProvider.Ins.DB.MAINTENANCEBILLs.Where(x => x.CUSTOMER.CUS_ID == cus.CUS_ID && x.PRODUCT.PRO_NAME.ToLower().Contains(txb.Text)).ToList();
                datagridOrderBill.ItemsSource = null;
                datagridOrderBill.ItemsSource = filterList;

            }
            else
            {
                datagridOrderBill.ItemsSource = DataProvider.Ins.DB.MAINTENANCEBILLs.Where( x => x.CUSTOMER_ID == cus.CUS_ID).ToList();
            }    
        }

        private void datagridOrderBill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void datagridOrderBill_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
