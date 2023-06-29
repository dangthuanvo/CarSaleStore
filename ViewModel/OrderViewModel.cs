using CarSalesSystem.Admin.Pages;
using CarSalesSystem.Admin.User_Controls;
using CarSalesSystem.Admin.Windows;
using CarSalesSystem.Customer.Windows;
using CarSalesSystem.Model;
using CarSalesSystem.Viewmodel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace CarSalesSystem.ViewModel
{
    public class OrderViewModel : BaseViewModel, INotifyPropertyChanged
    {
     

        private OrderPG order;
        private String idOrder;
        private String imagefilename;
        private OrderControl orderControl;
        public OrderPG orderPG { get => order; set => order = value; }
        public ICommand loadOrderCommand { get; set; }
        public ICommand EditOrderCommand { get; set; }
        //public ICommand UploadPictureCommand { get; set; }
        public ICommand ConfirmOrderCommand { get; set; }
        public ICommand UpdateInforCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand loadOrderWithFilterCommand { get; set; }
        public OrderViewModel()
        {
            loadOrderCommand = new RelayCommand<OrderPG>((parameter) => true, (parameter) => LoadOrder(parameter));
            loadOrderWithFilterCommand = new RelayCommand<OrderPG>((parameter) => true, (parameter) => loadOrderWithFilter(parameter));
            EditOrderCommand = new RelayCommand<OrderControl>((parameter) => true, (parameter) => ClickEditOrder(parameter));
            //UploadPictureCommand = new RelayCommand<Grid>((parameter) => true, (parameter) => UploadPicture(parameter));
            UpdateInforCommand = new RelayCommand<EditCustomer>((parameter) => true, (parameter) => UpdateInfor(parameter));
            BackCommand = new RelayCommand<EditCustomer>((parameter) => true, (parameter) => parameter.Close());
            ConfirmOrderCommand = new RelayCommand<OrderBillConfirm>((parameter) => true, (parameter) => ClickConfirmOrder(parameter));
        }
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
        private void ClickConfirmOrder(OrderBillConfirm parameter)
        {
            ORDERBILL order = DataProvider.Ins.DB.ORDERBILLs.Find(idOrder);
            var empinfo = DataProvider.Ins.DB.EMPLOYEEs.Find(AccountInfo.IdAccount);
            var customer = DataProvider.Ins.DB.CUSTOMERs.Find(order.CUSTOMER_ID.ToString());
            var product = DataProvider.Ins.DB.PRODUCTs.Find(order.PRO_ID);
            if (product.STORAGE_NUMBER == 0)
            {
                notifier.ShowError("Out of stock, please import first");
                return;
            }

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["NamConnection"].ConnectionString;
            try
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO SELLBILL (SB_ID, SB_DATEB,CUSTOMER_ID, PRO_ID, EMPLOYEE_ID, RANK_ID, TOTAL_PRICE) 
                    VALUES(@SB_ID, @SB_DATEB, @CUSTOMER_ID, @PRO_ID, @EMPLOYEE_ID, @RANK_ID, @TOTAL_PRICE)";
                    command.Parameters.AddWithValue("@SB_ID", order.OB_ID);
                    command.Parameters.AddWithValue("@SB_DATEB", DateTime.Now);
                    command.Parameters.AddWithValue("@CUSTOMER_ID", order.CUSTOMER_ID);
                    command.Parameters.AddWithValue("@PRO_ID", order.PRO_ID);
                    command.Parameters.AddWithValue("@EMPLOYEE_ID", empinfo.EMP_ID);
                    command.Parameters.AddWithValue("@RANK_ID", customer.RANK_ID);
                    command.Parameters.AddWithValue("@TOTAL_PRICE", order.TOTAL_PRICE);
                    command.ExecuteNonQuery();
                    DataProvider.Ins.DB.SaveChanges();
                    notifier.ShowSuccess("Confirm order successfully");
                }
            }
            catch (Exception exception)
            {
                //noti
                notifier.ShowError(exception.Message);
                return;
            }
            finally
            {
                parameter.Close();
                connection.Close();
            }
            customer.REVENUE += order.TOTAL_PRICE;
            order.PAID = 1;
            product.STORAGE_NUMBER -= 1;
            DataProvider.Ins.DB.SaveChanges();
            ResetRankCustomer(customer);
            LoadOrder(this.order);
        }
        private void ResetRankCustomer(CUSTOMER cus)
        {
            if (cus.REVENUE < 2000000)
                cus.RANK_ID = "R00";
            if (cus.REVENUE < 5000000 && cus.REVENUE >=2000000)
                cus.RANK_ID = "R01";
            if (cus.REVENUE < 10000000 && cus.REVENUE >= 5000000)
                cus.RANK_ID = "R02";
            if (cus.REVENUE >= 10000000)
                cus.RANK_ID = "R03";
            DataProvider.Ins.DB.SaveChanges();
        }
            private void loadOrderWithFilter(OrderPG parameter)
        {
            //parameter.skpOrderBill.Children.Clear();
               
            //if (parameter.searchboxName.Text.Length != 0)
            //{
            //     var listcustomerFilter = DataProvider.Ins.DB.CUSTOMERs.Where(x => x.CUS_NAME.ToUpper().StartsWith(parameter.searchboxName.Text.ToUpper()));
            //    if (parameter.cbRank.SelectedIndex > 0)
            //    {
            //        int type = int.Parse(s.Content.ToString());
            //        listcustomerFilter.Where(x => x.RANK_MONEY.RANK_TYPE == type);
            //    }
            //    bool flat = false;
            //    int i = 1;
            //    foreach (var item in listcustomerFilter)
            //    {
            //        OrderControl infCustomer = new OrderControl();
            //        flat = !flat;
            //        if (flat)
            //        {
            //            infCustomer.grMain.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFFFF");
            //        }
            //        //infCustomer.tbName.Text = item.CUS_NAME;
            //        infCustomer.tbNo.Text = item.CUS_ID;
            //        parameter.skpCustomer.Children.Add(infCustomer);
            //        i++;
            //    }
            //}
            //else
            //{
            //    if (parameter.cbRank.SelectedIndex > 0)
            //    {
            //        int type = int.Parse(s.Content.ToString());
            //        var listcustomerFilter = DataProvider.Ins.DB.CUSTOMERs.Where(x => x.RANK_MONEY.RANK_TYPE == type).ToList();
            //        bool flat = false;
            //        int i = 1;
            //        foreach (var item in listcustomerFilter)
            //        {
            //            OrderControl infCustomer = new OrderControl();
            //            flat = !flat;
            //            if (flat)
            //            {
            //                infCustomer.grMain.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFFFF");
            //            }
            //            //infCustomer.tbName.Text = item.CUS_NAME;
            //            infCustomer.tbNo.Text = item.CUS_ID;
            //            parameter.skpCustomer.Children.Add(infCustomer);
            //            i++;
            //        }
            //    }
            //    else
            //    {
            //        LoadOrder(orderPG);
            //    }
               
               
            //}
        }

        private void UpdateInfor(EditCustomer parameter)
        {
            var customerInfo = DataProvider.Ins.DB.CUSTOMERs.Find(idOrder);
            customerInfo.CUS_NAME = parameter.tbFullName.Text;
            customerInfo.CUS_ADDRESS = parameter.tbAddress.Text;
            customerInfo.PHONE = parameter.tbTelephone.Text;
            customerInfo.CUS_ADDRESS = parameter.tbAddress.Text;
            customerInfo.GENDER = parameter.cbGender.Text;
            customerInfo.CUS_DATE_OF_BIRTH = DateTime.Parse(parameter.dpBirth.Text);
            if(imagefilename != null)
            {
                customerInfo.IMG = Converter.Instance.StreamFile(imagefilename);
            }
            DataProvider.Ins.DB.SaveChanges();
            notifier.ShowSuccess("Update Information Successfully");
            LoadOrder(orderPG);
            parameter.Close();

        }


        private void ClickEditOrder(OrderControl parameter)
        {
            this.orderControl = parameter;
            idOrder = parameter.tbNo.Text;
            showEditOrder(idOrder);
        }
        private decimal CalculateCashSale(CUSTOMER cus, PRODUCT pro)
        {
            float discount = (float)cus.RANK_MONEY.DISCOUNT / 100;
            decimal totalPrice = pro.PRICE * (decimal)discount * 1;
            if (cus.RANK_MONEY.CASH_LIMIT < totalPrice)
            {
                return (decimal)cus.RANK_MONEY.CASH_LIMIT;
            }
            else return totalPrice;
        }
        public void showEditOrder(String idOrder)
        {
            OrderBillConfirm editOrderwindow = new OrderBillConfirm();
            var orderInfo = DataProvider.Ins.DB.ORDERBILLs.Find(idOrder);
            PRODUCT product = DataProvider.Ins.DB.PRODUCTs.Find(orderInfo.PRO_ID.ToString());
            CUSTOMER customer = DataProvider.Ins.DB.CUSTOMERs.Find(orderInfo.CUSTOMER_ID.ToString());
            editOrderwindow.txtTenKH.Text = customer.CUS_NAME;
            editOrderwindow.txtTenSP.Text = product.PRO_NAME;
            editOrderwindow.txtNgayDatHang.SelectedDate = orderInfo.OB_DATEB;
            editOrderwindow.txtDiscount.Text = customer.RANK_MONEY.DISCOUNT.ToString() + "%";
            decimal cashSale = CalculateCashSale(customer, product);
            editOrderwindow.txtCashSale.Text = String.Format("{0:0,0}", cashSale);
            editOrderwindow.txtPrice.Text = String.Format("{0:0,0}", orderInfo.TOTAL_PRICE);
            if (product.IMG != null)
            {
                Stream StreamObj = new MemoryStream(product.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();
                editOrderwindow.imgCarOrder.Source = BitObj;
            }
            editOrderwindow.ShowDialog();

        }

        private void LoadOrder(OrderPG parameter)
        {
            this.order = parameter;
            parameter.skpOrderBill.Children.Clear();

            var listOrder = DataProvider.Ins.DB.ORDERBILLs.ToList();
            bool flat = false;
            int i = 1;
            foreach (var item in listOrder)
            {
                if (item.PAID == 0)
                    continue;
                OrderControl infOrder = new OrderControl();
                flat = !flat;
                if (flat)
                {
                    infOrder.grMain.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                }
                infOrder.tbNo.Text = item.OB_ID;
                infOrder.tbCustomer.Text = item.CUSTOMER_ID;
                infOrder.tbProduct.Text = item.PRO_ID;
                infOrder.tbQuantity.Text = item.QUANTITY.ToString();
                infOrder.tbTotalPrice.Text = item.TOTAL_PRICE.ToString();
                infOrder.tbStatus.Text = "Paid";
                parameter.skpOrderBill.Children.Add(infOrder);      
                i++;
            }
            foreach (var item in listOrder)
            {
                if (item.PAID == 1)
                    continue;
                OrderControl infOrder = new OrderControl();
                flat = !flat;
                if (flat)
                {
                    infOrder.grMain.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                }
                infOrder.tbNo.Text = item.OB_ID;
                infOrder.tbCustomer.Text = item.CUSTOMER_ID;
                infOrder.tbProduct.Text = item.PRO_ID;
                infOrder.tbQuantity.Text = item.QUANTITY.ToString();
                infOrder.tbTotalPrice.Text = item.TOTAL_PRICE.ToString();
                infOrder.tbStatus.Text = "Pending";
                parameter.skpOrderBill.Children.Add(infOrder);
                i++;
            }
        }
    }
}