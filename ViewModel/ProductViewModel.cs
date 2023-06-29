using CarSalesSystem.Admin.Pages;
using CarSalesSystem.Admin.User_Controls;
using CarSalesSystem.Admin.Windows;
using CarSalesSystem.Model;
using CarSalesSystem.Viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

namespace CarSalesSystem.ViewModel
{
    public class ProductViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ProductControl productControl;
        private ImportControl importControl;
        private CheckOrderUserControl checkOrderUserControl;
        private CheckCompleteMaintainceControl checkCompleteMaintainceControl;
        private int checkNewAccount = 0;
        private int numberproduct;
        private ProductPG productPG;
        private WarehousePG warehousePG;
        private Addproduct Addproduct;
        private ListOfOrder ListOfOrderwd;
        public ListOfOrder listoforderwd { get =>ListOfOrderwd; set => ListOfOrderwd = value;}

        private ListOfMaintaince listOfmaintaincewd;
        public ListOfMaintaince ListofMaintaincewd { get => listOfmaintaincewd; set => listOfmaintaincewd = value; }

        private String imagefilename;
        private String idProduct;
        private String idOrderBill;
        private String idMaintaineBill;
        private String idimportreceipt;
        public ICommand EditProductCommand { get; set; }
        public ICommand ShowLoadProductCommand { get; set; }
        public ICommand ClickAddCommand { get; set; }
        public ICommand BackAddProductCommand { get; set; }
        public ICommand BackEditProductCommand { get; set; }
        public ICommand UpdateProductCommand { get; set; }
        public ICommand UpLoadIMGEditCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand ShowLoadImportProductCommand { get; set; }
        public ICommand ClickImportProductCommand { get; set; }
        public ICommand BackImportProductCommand { get; set; }
        public ICommand  CaculateTotalCommand { get; set; }
        public ICommand ImportProductSaveCommand { get; set; }
        public ICommand BuyProductCommand { get; set; }
        public ICommand SearchCustomerInfoCommand { get; set; }
        public ICommand AddCustomerInfoCommand { get; set; }
        public ICommand CaculatePriceProductCommand { get; set; }
        public ICommand BuyProductWithBillCommand { get; set; }
        public ICommand LoadFilterProductCommand { get; set; }
        public ICommand LoadFilterProductWareHouseCommand { get; set; }
        public ICommand loadOrderBillCommand { get; set; }
        public ICommand showListOrderBillCommand { get; set; }
        public ICommand CheckBuyProductEmpCommand { get; set; }

        public ICommand loadMaintaineBillCommand { get; set; }
        public ICommand ShowListMainTainCommand { get; set; }
        public ICommand ClickEditMoneyFeeCommand { get; set; }
        public ICommand ConfirmTotalFeeMaintainBillCommand { get; set; }
        public ICommand ShowUpdateAndAddProducerCommand { get; set; }
        public ICommand ShowHistoryImportCommand { get; set; }
        public ICommand ShowImportReceiptInfoCommand { get; set; }
        public ProductViewModel()
        {
            EditProductCommand = new RelayCommand<ProductControl>((parameter) => true, (parameter) => ClickEditProduct(parameter));
            BuyProductCommand = new RelayCommand<ProductControl>((parameter) => true, (parameter) => ClickBuyProduct(parameter));

            ShowLoadProductCommand = new RelayCommand<ProductPG>((parameter) => true, (parameter) => ShowLoadProduct(parameter));
            ClickAddCommand = new RelayCommand<ProductPG>((parameter) => true, (parameter) => ShowAddProduct(parameter));
            LoadFilterProductCommand = new RelayCommand<ProductPG>((parameter) => true, (parameter) => LoadFilterProduct(parameter));
            ShowListMainTainCommand = new RelayCommand<ProductPG>((parameter) => true, (parameter) => showListMaintainceBill(parameter));
            showListOrderBillCommand = new RelayCommand<ProductPG>((parameter) => true, (parameter) => showListOrderBill(parameter));
      

            BackAddProductCommand = new RelayCommand<Addproduct>((parameter) => true, (parameter) => parameter.Close());
            AddProductCommand = new RelayCommand<Addproduct>((parameter) => true, (parameter) => AddProduct(parameter));


            BackEditProductCommand = new RelayCommand<EditProduct>((parameter) => true, (parameter) => parameter.Close());
            UpdateProductCommand = new RelayCommand<EditProduct>((parameter) => true, (parameter) => UpdateProduct(parameter));

            UpLoadIMGEditCommand = new RelayCommand<Grid>((parameter) => true, (parameter) => UpLoadIMGEdit(parameter));

            BackImportProductCommand = new RelayCommand<ImportProduct>((parameter) => true, (parameter) => parameter.Close());

            ShowHistoryImportCommand = new RelayCommand<WarehousePG>((parameter) => true, (parameter) => ShowHistoryImport(parameter));
            ShowLoadImportProductCommand = new RelayCommand<WarehousePG>((parameter) => true, (parameter) => ShowLoadImportProduct(parameter));
            LoadFilterProductWareHouseCommand = new RelayCommand<WarehousePG>((parameter) => true, (parameter) => LoadFilterProductWareHouse(parameter));

            ClickImportProductCommand = new RelayCommand<ImportControl>((parameter) => true, (parameter) => ShowImportProduct(parameter));
            CaculateTotalCommand = new RelayCommand<ImportProduct>((parameter) => true, (parameter) => CaculateTotal(parameter));
            ImportProductSaveCommand = new RelayCommand<ImportProduct>((parameter) => true, (parameter) => ImportProductSave(parameter));

            SearchCustomerInfoCommand = new RelayCommand<BuyProductEmp>((parameter) => true, (parameter) => SearchCustomerInfo(parameter));
            AddCustomerInfoCommand = new RelayCommand<BuyProductEmp>((parameter) => true, (parameter) => AddCustomerInfo(parameter));
            CaculatePriceProductCommand = new RelayCommand<BuyProductEmp>((parameter) => true, (parameter) => CaculatePriceProduct(parameter));
            BuyProductWithBillCommand = new RelayCommand<BuyProductEmp>((parameter) => true, (parameter) => BuyProductWithBill(parameter));
            loadOrderBillCommand = new RelayCommand<ListOfOrder>((parameter) => true, (parameter) => loadOrderBill(parameter));
            loadMaintaineBillCommand = new RelayCommand<ListOfMaintaince>((parameter) => true, (parameter) => loadMaintainceBill(parameter));
            CheckBuyProductEmpCommand = new RelayCommand<CheckOrderUserControl>((parameter) => true, (parameter) => ClickBuyProductEmp(parameter));
            ClickEditMoneyFeeCommand = new RelayCommand<CheckCompleteMaintainceControl>((parameter) => true, (parameter) => ClickEditMoneyFee(parameter));
            ConfirmTotalFeeMaintainBillCommand = new RelayCommand<TotalFeeMaintaine>((parameter) => true, (parameter) => ConfirmTotalFeeMaintainBill(parameter));
            ShowUpdateAndAddProducerCommand = new RelayCommand<ProductPG>((parameter) => true, (parameter) => ShowUpdateAndAddProducer(parameter));
            ShowImportReceiptInfoCommand = new RelayCommand<ImportReceiptControl>((parameter) => true, (parameter) => ClickShowImportReceiptInfo(parameter));
        }

        private void ClickShowImportReceiptInfo(ImportReceiptControl parameter)
        {
            var item = DataProvider.Ins.DB.IMPORTRECEIPTINFOes.Find(parameter.tbNo.Text);

            if (item != null)
            {
                InfoImportWindow i = new InfoImportWindow();
                i.txbIdStock.Text = item.IRECEIPT_ID;
                i.txbIdGood.Text = item.PRO_ID;
                i.txbQuantity.Text = item.QUANTITY.ToString();
                double k = (double)item.IMPORT_PRICE;
                i.txbImportPrice.Text = k.ToString("C", CultureInfo.CurrentCulture);
                i.ShowDialog();
            }
        }

        private void ShowHistoryImport(WarehousePG parameter)
        {
            var listimport = DataProvider.Ins.DB.IMPORTRECEIPTs.ToList();
            ListImportWindow lis = new ListImportWindow();
            lis.skpImportBill.Children.Clear();
            foreach(var item in listimport)
            {
                ImportReceiptControl i = new ImportReceiptControl();
                i.tbNo.Text = item.IRECEIPT_ID.ToString();
                i.tbEmpId.Text = item.EMPLOYEE_ID.ToString();
                i.tbDate.Text = item.DATETIME_IMPORT.ToString();
                double k  =(double) item.TOTAL_PRICE;
                i.TotalPrice.Text= k.ToString("C",CultureInfo.CurrentCulture);
                lis.skpImportBill.Children.Add(i);
            }
            lis.ShowDialog();
            
        }

        private void ShowUpdateAndAddProducer(ProductPG parameter)
        {
            AddAndUpdateProducer addAndUpdateProducer = new AddAndUpdateProducer();
            var listid = DataProvider.Ins.DB.PRODUCERs.ToList();
            addAndUpdateProducer.cbIdProducer.Items.Add("NEW");
            foreach (var item in listid)
            {
                addAndUpdateProducer.cbIdProducer.Items.Add(item.PRODUCER_ID);
            }
            addAndUpdateProducer.ShowDialog();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: System.Windows.Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;
        });

        private void ConfirmTotalFeeMaintainBill(TotalFeeMaintaine parameter)
        {
            var item = DataProvider.Ins.DB.MAINTENANCEBILLs.Find(idMaintaineBill);
            item.TOTALFEE = Decimal.Parse(parameter.tbMoney.Text);
            item.BILL_STATUS = "DONE";
            DataProvider.Ins.DB.SaveChanges();
            loadMaintainceBill(ListofMaintaincewd);
            parameter.Close();
            
        }

        private void ClickEditMoneyFee(CheckCompleteMaintainceControl parameter)
        {
            this.checkCompleteMaintainceControl = parameter;
            idMaintaineBill = parameter.tbNo.Text;
            TotalFeeMaintaine totalFee = new TotalFeeMaintaine();
            var item = DataProvider.Ins.DB.MAINTENANCEBILLs.Find(idMaintaineBill);
            totalFee.tbMoney.Text = item.TOTALFEE.ToString();
            totalFee.ShowDialog();
        }
        private void loadMaintainceBill(ListOfMaintaince parameter)
        {
            this.listOfmaintaincewd = parameter;
            
            if (parameter.cbCompleteOrUn.SelectedIndex == 0)
            {
                parameter.skpOrderBill.Children.Clear();
                var listMaintaince = DataProvider.Ins.DB.MAINTENANCEBILLs.Where(x => x.BILL_STATUS != "DONE").ToList();
                foreach (var item in listMaintaince)
                {
                    CheckCompleteMaintainceControl check = new CheckCompleteMaintainceControl ();
                    check.tbNo.Text = item.MB_ID;
                    check.tbDate.Text = item.MB_DATE.ToString();
                    check.tbCustomerID.Text = item.CUSTOMER_ID.ToString();
                    check.tbProductID.Text = item.PRO_ID.ToString();
                    double s = (double)item.TOTALFEE;
                    check.tbTotalPrice.Text = s.ToString("C", CultureInfo.CurrentCulture);
                    check.btncheckcompleteBill.Visibility = System.Windows.Visibility.Visible;
                    check.tbEmp.Visibility = System.Windows.Visibility.Hidden;
                    parameter.skpOrderBill.Children.Add(check);
                }
            }
            else
            {
                parameter.skpOrderBill.Children.Clear();
                var listMaintaince = DataProvider.Ins.DB.MAINTENANCEBILLs.Where(x=>x.BILL_STATUS =="DONE").ToList();
                foreach (var item in listMaintaince)
                {
                    CheckCompleteMaintainceControl check = new CheckCompleteMaintainceControl();
                    check.tbNo.Text = item.MB_ID;
                    check.tbDate.Text = item.MB_DATE.ToString();
                    check.tbCustomerID.Text = item.CUSTOMER_ID.ToString();
                    check.tbProductID.Text = item.PRO_ID.ToString();
                    double s = (double)item.TOTALFEE;
                    check.tbTotalPrice.Text = s.ToString("C", CultureInfo.CurrentCulture);
                    check.btncheckcompleteBill.Visibility = System.Windows.Visibility.Hidden;
                    check.tbEmp.Visibility = System.Windows.Visibility.Visible;
                    parameter.skpOrderBill.Children.Add(check);
                }
            }

        }

        private void showListMaintainceBill(ProductPG parameter)
        {
            ListOfMaintaince listOfMaintaince = new ListOfMaintaince();
            listOfMaintaince.cbCompleteOrUn.SelectedIndex = 0;
            listOfMaintaince.ShowDialog();
        }

        private void ClickBuyProductEmp(CheckOrderUserControl parameter)
        {
            this.checkOrderUserControl = parameter;
            idOrderBill = parameter.tbNo.Text;
            CheckBuyProductEmp(idOrderBill);
            ListOfOrderwd.skpOrderBill.Children.Remove(parameter);
            
        }

        private void CheckBuyProductEmp(string idOrderBill)
        {
            var item = new SELLBILL();
            var itemOrder = DataProvider.Ins.DB.ORDERBILLs.Find(idOrderBill);
            var listSell = DataProvider.Ins.DB.SELLBILLs.ToList();
            var product = DataProvider.Ins.DB.PRODUCTs.Find(itemOrder.PRO_ID);
            if (product.STORAGE_NUMBER == 0)
            {
                notifier.ShowError("Out of stock, please import first");
                return;
            }
            int k = listSell.Count();
            string id = "";
            if (k < 10)
            {
                id = "BH0" + k.ToString();
            }
            else
            {
                id = "BH" + k.ToString();
            }
            item.SB_ID = id;
            item.CUSTOMER_ID = itemOrder.CUSTOMER_ID;
            item.PRO_ID = itemOrder.PRO_ID;
            item.SB_DATEB = itemOrder.OB_DATEB;
            item.TOTAL_PRICE = itemOrder.TOTAL_PRICE;
            item.EMPLOYEE_ID = AccountInfo.IdAccount;
            DataProvider.Ins.DB.SELLBILLs.Add(item);
            //DataProvider.Ins.DB.ORDERBILLs.Remove(itemOrder);
            itemOrder.PAID = 1;
            product.STORAGE_NUMBER -= 1;
            DataProvider.Ins.DB.SaveChanges();
        }

        private void showListOrderBill(ProductPG parameter)
        {
            ListOfOrder lsoforder = new ListOfOrder();
            lsoforder.cbSellAndOrder.SelectedIndex = 0;
            lsoforder.ShowDialog();

        }

        private void loadOrderBill(ListOfOrder parameter)
        {
            this.listoforderwd = parameter;
            if (parameter.cbSellAndOrder.SelectedIndex == 0)
            {
                parameter.skpOrderBill.Children.Clear();
                var listOrder = DataProvider.Ins.DB.ORDERBILLs;
                foreach (var item in listOrder)
                {
                    if (item.PAID == 0)
                    {
                        CheckOrderUserControl check = new CheckOrderUserControl();
                        check.tbNo.Text = item.OB_ID;
                        check.tbDate.Text = item.OB_DATEB.ToString();
                        check.tbCustomerID.Text = item.CUSTOMER_ID.ToString();
                        check.tbProductID.Text = item.PRO_ID.ToString();
                        double s = (double)item.TOTAL_PRICE;
                        check.tbTotalPrice.Text = s.ToString("C", CultureInfo.CurrentCulture);
                        check.btncheckOrderBill.Visibility = System.Windows.Visibility.Visible;
                        check.tbEmp.Visibility = System.Windows.Visibility.Hidden;
                        parameter.empLabel.Visibility = System.Windows.Visibility.Hidden;
                        parameter.skpOrderBill.Children.Add(check);
                    }
                }
            }
            else
            {
                parameter.skpOrderBill.Children.Clear();
                var listSell = DataProvider.Ins.DB.SELLBILLs.ToList();
                foreach (var item in listSell)
                {
                    //CheckOrderUserControl check = new CheckOrderUserControl();
                    //check.tbNo.Text = item.SB_ID;
                    //check.tbDate.Text = item.SB_DATEB.ToString();
                    //check.tbCustomerID.Text = item.CUSTOMER_ID.ToString();
                    //check.tbProductID.Text = item.PRO_ID.ToString();
                    //double s = (double)item.TOTAL_PRICE;
                    //check.tbTotalPrice.Text = s.ToString("C", CultureInfo.CurrentCulture);
                    //check.btncheckOrderBill.Visibility = System.Windows.Visibility.Hidden;
                    //check.tbEmp.Text = item.EMPLOYEE_ID;
                    //check.tbEmp.Visibility = System.Windows.Visibility.Visible;
                    //parameter.skpOrderBill.Children.Add(check);

                    CheckOrderUserControl check = new CheckOrderUserControl();
                    check.tbNo.Text = item.SB_ID;
                    check.tbDate.Text = item.SB_DATEB.ToString();
                    check.tbCustomerID.Text = item.CUSTOMER_ID.ToString();
                    check.tbProductID.Text = item.PRO_ID.ToString();
                    double s = (double)item.TOTAL_PRICE;
                    check.tbTotalPrice.Text = s.ToString("C", CultureInfo.CurrentCulture);
                    check.btncheckOrderBill.Visibility = System.Windows.Visibility.Hidden;
                    check.tbEmp.Text = item.EMPLOYEE_ID;
                    check.tbEmp.Visibility = System.Windows.Visibility.Visible;
                    parameter.empLabel.Visibility= System.Windows.Visibility.Visible;
                    parameter.skpOrderBill.Children.Add(check);

                }
            }
           
        }

        private void LoadFilterProductWareHouse(WarehousePG parameter)
        {
            parameter.skpProduct.Children.Clear();
            ComboBoxItem s = (ComboBoxItem)parameter.cbNumber.SelectedItem;
            if (parameter.searchboxAmountProduct.Text.Length != 0)
            {
                var listProduct = DataProvider.Ins.DB.PRODUCTs.Where(x => x.PRO_NAME.ToUpper().StartsWith(parameter.searchboxAmountProduct.Text.ToUpper()));
                if (parameter.cbNumber.SelectedIndex > 0)
                {
                    int k = parameter.cbNumber.SelectedIndex;
                    
                    if (k == 1)
                    {
                        listProduct.Where(x => (x.STORAGE_NUMBER - x.SELL_NUMBER) <= 5).ToList(); 
                    }
                    else
                    if (k == 2)
                    {
                        listProduct.Where(x => (x.STORAGE_NUMBER - x.SELL_NUMBER) <= 10).ToList();
                    }
                    else
                    if (k == 3)
                    {
                        listProduct.Where(x => (x.STORAGE_NUMBER - x.SELL_NUMBER) > 10).ToList();
                    }

                }
                bool flat = false;
                int i = 0;
                foreach (var product in listProduct)
                {
                    ImportControl productControl = new ImportControl();
                    flat = !flat;
                    if (flat)
                    {
                        productControl.grMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                    }
                    productControl.tbName.Text = product.PRO_NAME;
                    productControl.tbNo.Text = product.PRO_ID;
                    productControl.tbYear.Text = product.PRO_YEAR.ToString();
                    productControl.tbAmount.Text = (product.STORAGE_NUMBER - product.SELL_NUMBER).ToString();
                    parameter.skpProduct.Children.Add(productControl);
                    i++;
                }
                numberproduct = i;
            }
            else
            {
                if (parameter.cbNumber.SelectedIndex > 0)
                {
                    int k = parameter.cbNumber.SelectedIndex;
                    List<PRODUCT> listproduct = new List<PRODUCT>();
                    if (k == 1)
                    {
                        listproduct = DataProvider.Ins.DB.PRODUCTs.Where(x => (x.STORAGE_NUMBER - x.SELL_NUMBER) <= 5).ToList();
                    }
                    if (k == 2)
                    {
                        listproduct = DataProvider.Ins.DB.PRODUCTs.Where(x => (x.STORAGE_NUMBER - x.SELL_NUMBER) <= 10).ToList();
                    }
                    if (k == 3)
                    {
                        listproduct = DataProvider.Ins.DB.PRODUCTs.Where(x => (x.STORAGE_NUMBER - x.SELL_NUMBER) > 10).ToList();
                    }
                    bool flat = false;
                    int i = 0;
                    foreach (var product in listproduct)
                    {
                        ImportControl productControl = new ImportControl();
                        flat = !flat;
                        if (flat)
                        {
                            productControl.grMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                        }
                        productControl.tbName.Text = product.PRO_NAME;
                        productControl.tbNo.Text = product.PRO_ID;
                        productControl.tbYear.Text = product.PRO_YEAR.ToString();
                        productControl.tbAmount.Text = (product.STORAGE_NUMBER - product.SELL_NUMBER).ToString();
                        parameter.skpProduct.Children.Add(productControl);
                        i++;
                    }
                    numberproduct = i;

                }
                else ShowLoadImportProduct(warehousePG);

              
            }
        }

        private void LoadFilterProduct(ProductPG parameter)
        {
            parameter.skpProduct.Children.Clear();
            if(parameter.searchBoxProduct.Text.Length != 0)
            {
                var listProduct = DataProvider.Ins.DB.PRODUCTs.Where(x => x.PRO_NAME.ToUpper().StartsWith(parameter.searchBoxProduct.Text.ToUpper())).ToList();

                bool flat = false;
                int i = 0;
                foreach (var product in listProduct)
                {
                    ProductControl productControl = new ProductControl();
                    flat = !flat;
                    if (flat)
                    {
                        productControl.grMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                    }
                    productControl.tbName.Text = product.PRO_NAME;
                    productControl.tbNo.Text = product.PRO_ID;
                    productControl.tbType.Text = product.TYPEPRODUCT.TYPEPRODUCT_NAME.ToString();
                    productControl.tbYear.Text = product.PRO_YEAR.ToString();
                    parameter.skpProduct.Children.Add(productControl);
                    i++;
                }
                numberproduct = i;
            }else
            ShowLoadProduct(productPG);
        }

        private void BuyProductWithBill(BuyProductEmp parameter)
        {
            int count = DataProvider.Ins.DB.SELLBILLs.Count() +1;
            string id = "";
            if (count < 10) id = "000" + count.ToString();
            else  if (count <100) id = "00"+count.ToString();
            else if (count <1000) id = "0" + count.ToString();
            else id = count.ToString();
            var item = new SELLBILL();
            item.SB_DATEB = DateTime.Parse(parameter.pdSell.Text);
            item.SB_ID = id;
            item.PRO_ID = idProduct;
            decimal value;
            decimal.TryParse(parameter.tbTotalBill.Text, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out value);
            item.TOTAL_PRICE = value;
            item.EMPLOYEE_ID = AccountInfo.IdAccount;
            var cusInfo = DataProvider.Ins.DB.CUSTOMERs.Where(x => x.PHONE == parameter.tbPhone.Text).FirstOrDefault();
            item.CUSTOMER_ID = cusInfo.CUS_ID;
            item.RANK_ID= cusInfo.RANK_ID;
            DataProvider.Ins.DB.SELLBILLs.Add(item);
            item.PRODUCT.SELL_NUMBER += +1;
            decimal value_priceproduct;
            decimal.TryParse(parameter.tbPriceProduct.Text, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out value_priceproduct);
            cusInfo.REVENUE += value_priceproduct ;
            cusInfo.PRODUCT_NUMBER += +1;
            DataProvider.Ins.DB.SaveChanges();
            UpdateRankForCustomer(cusInfo.CUS_ID);
            parameter.Close();
            notifier.ShowSuccess("Buy Successfully");

        }

        private void UpdateRankForCustomer(string idcus)
        {
            var cus = DataProvider.Ins.DB.CUSTOMERs.Find(idcus);
            var list = DataProvider.Ins.DB.RANK_MONEY.ToList();
            foreach(var item in list)
            {
                if (cus.REVENUE>= item.CASH_LIMIT)
                {
                    cus.RANK_ID = item.RANK_ID;
                }
            }
            DataProvider.Ins.DB.SaveChanges();
        }

        private void CaculatePriceProduct(BuyProductEmp parameter)
        {
            decimal price;
            decimal.TryParse(parameter.tbPriceProduct.Text, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out price);
            int percent = int.Parse(parameter.tbDiscount.Text);
            if (percent != 0)
            {
                decimal discount = (decimal)(price * percent) / 100;
                decimal tong = Math.Truncate(price - discount);
                parameter.tbTotalBill.Text = (tong).ToString("C", CultureInfo.CurrentCulture);
            }
            else parameter.tbTotalBill.Text = (price).ToString("C", CultureInfo.CurrentCulture);
            parameter.btnBuyItemBill.Visibility=System.Windows.Visibility.Visible;

        }


        private void AddCustomerInfo(BuyProductEmp parameter)
        {
            if (checkNewAccount == 0)
            {
               
                ACCOUNT account = new ACCOUNT();
                account.USERNAME = parameter.tbPhone.Text;
                account.PASS = "123";
                account.TYPE_USER = 2;
                try
                {
                    DataProvider.Ins.DB.ACCOUNTs.Add(account);
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                if(parameter.tbName.Text.Length == 0)
                {
                    parameter.tbName.Focus();
                }
                else 
                if(parameter.tbAddress.Text.Length == 0)
                {
                    parameter.tbAddress.Focus();
                }
                else if (parameter.tbPhone.Text.Length == 0)
                {
                    parameter.tbPhone.Focus();
                }
                else
                {
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["NamConnection"].ConnectionString;
                    try
                    {
                        connection.Open();
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = @"INSERT INTO CUSTOMER(CUS_ACCOUNT,CUS_NAME,PHONE,CUS_ADDRESS,RANK_ID,REGIST_DATE,REVENUE,PRODUCT_NUMBER) 
                        VALUES(@CUS_ACCOUNT,@CUS_NAME,@PHONE,@CUS_ADDRESS,@RANK_ID,@REGIST_DATE,@REVENUE,@PRODUCT_NUMBER)";
                            command.Parameters.AddWithValue("@CUS_ACCOUNT", parameter.tbPhone.Text);
                            command.Parameters.AddWithValue("@CUS_NAME", parameter.tbName.Text);
                            command.Parameters.AddWithValue("@PHONE", parameter.tbPhone.Text);
                            command.Parameters.AddWithValue("@CUS_ADDRESS", parameter.tbAddress.Text);
                            command.Parameters.AddWithValue("@RANK_ID", "R00");
                            command.Parameters.AddWithValue("@REGIST_DATE", DateTime.UtcNow.ToString());
                            command.Parameters.AddWithValue("@REVENUE", 0);
                            command.Parameters.AddWithValue("@PRODUCT_NUMBER", 0);
                            command.ExecuteNonQuery();
                            notifier.ShowSuccess("Add Successfully");
                        }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                
              
            }
        }

        private void SearchCustomerInfo(BuyProductEmp parameter)
        {
            var cusInfo = DataProvider.Ins.DB.CUSTOMERs.Where(x => x.PHONE == parameter.tbPhone.Text ).FirstOrDefault();
            if (cusInfo != null)
            {
                parameter.tbName.Text = cusInfo.CUS_NAME;
                parameter.tbAddress.Text = cusInfo.CUS_ADDRESS;
                parameter.tbDiscount.Text = cusInfo.RANK_MONEY.DISCOUNT.ToString();
                checkNewAccount = 1;
            }
            else
            {
                checkNewAccount = 0;
                parameter.btnAddCusToList.IsEnabled = true;
                parameter.tbDiscount.Text = "0";
            }
        }

        private void ClickBuyProduct(ProductControl parameter)
        {
            this.productControl = parameter;
            idProduct = parameter.tbNo.Text;
            var productInfo = DataProvider.Ins.DB.PRODUCTs.Find(idProduct);

            ShowBuyProduct(idProduct);
        }

        private void ShowBuyProduct(string idProduct)
        {
            BuyProductEmp buyProduct = new BuyProductEmp();
            buyProduct.Title = "Buy Product";
            var productInfo = DataProvider.Ins.DB.PRODUCTs.Find(idProduct);
            buyProduct.pdSell.Text = DateTime.UtcNow.Date.ToString();
            var product = DataProvider.Ins.DB.PRODUCTs.Find(idProduct);
            buyProduct.tbNameProduct.Text = product.PRO_NAME;
            buyProduct.tbAmountProduct.Text = "1";
            buyProduct.tbPriceProduct.Text = product.PRICE.ToString("C", CultureInfo.CurrentCulture);
            buyProduct.ShowDialog();
        }

        private void ImportProductSave(ImportProduct parameter)
        {
            IMPORTRECEIPT importreceipt = new IMPORTRECEIPT();
            IMPORTRECEIPTINFO importreceiptinfo = new IMPORTRECEIPTINFO();
            var product = DataProvider.Ins.DB.PRODUCTs.Find(parameter.tbIdProduct.Text);
            product.STORAGE_NUMBER += int.Parse(parameter.tbQuantity.Text);
            importreceipt.IRECEIPT_ID = parameter.tbIdReceipt.Text;
            importreceipt.EMPLOYEE_ID = parameter.tbIdEmployee.Text;
            importreceipt.DATETIME_IMPORT = DateTime.Parse(parameter.pdDateTime.Text);
            decimal value;
            decimal.TryParse(parameter.tbTotalPrice.Text, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out value);
            importreceipt.TOTAL_PRICE = value;

            importreceiptinfo.IRECEIPT_ID = parameter.tbIdReceipt.Text;
            importreceiptinfo.PRO_ID=parameter.tbIdProduct.Text;
            importreceiptinfo.QUANTITY = int.Parse(parameter.tbQuantity.Text);
            importreceiptinfo.IMPORT_PRICE = decimal.Parse(parameter.tbImportPrice.Text);
            try
            {
                DataProvider.Ins.DB.IMPORTRECEIPTs.Add(importreceipt);
                DataProvider.Ins.DB.IMPORTRECEIPTINFOes.Add(importreceiptinfo);
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            ShowLoadImportProduct(warehousePG);
            notifier.ShowSuccess("Import Successfully");
            parameter.Close();
        }

        private void CaculateTotal(ImportProduct parameter)
        {
            long price=0;
            int quantity = 0;
            int.TryParse(parameter.tbImportPrice.Text, out quantity);
            long.TryParse(parameter.tbQuantity.Text, out price);
            parameter.tbTotalPrice.Text = (price*quantity).ToString("C", CultureInfo.CurrentCulture);
            
        }

        private void ShowImportProduct(ImportControl parameter)
        {
            this.importControl = parameter;
            idProduct = parameter.tbNo.Text;
            showImportProductWindow(idProduct);
        }

        private void showImportProductWindow(string idProduct)
        {
            var productInfo = DataProvider.Ins.DB.PRODUCTs.Find(idProduct);
            var listReceipt = DataProvider.Ins.DB.IMPORTRECEIPTs.Count() + 1;
            ImportProduct importProduct = new ImportProduct();
            importProduct.Title = "Import Product";
            importProduct.tbName.Text = productInfo.PRO_NAME;
            importProduct.tbYear.Text = productInfo.PRO_YEAR.ToString();
            importProduct.pdDateTime.Text = DateTime.UtcNow.Date.ToString();
            if (listReceipt < 10)
            {
                importProduct.tbIdReceipt.Text = "NH0" + listReceipt.ToString();
            }
            else
            {
                importProduct.tbIdReceipt.Text = "NH" + listReceipt.ToString();
            }
            importProduct.tbIdProduct.Text = idProduct;
            if (productInfo.IMG != null)
            {
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = Converter.Instance.ToImage(productInfo.IMG);
                importProduct.grdSelectImage.Background = imageBrush;
            }
            importProduct.tbIdEmployee.Text = AccountInfo.IdAccount;
            importProduct.ShowDialog();

        }

        private void ShowLoadImportProduct(WarehousePG parameter)
        {
            this.warehousePG = parameter;
            parameter.skpProduct.Children.Clear();
            var listProduct = DataProvider.Ins.DB.PRODUCTs.ToList();
            bool flat = false;
            int i = 0;
            foreach (var product in listProduct)
            {
                ImportControl productControl = new ImportControl();
                flat = !flat;
                if (flat)
                {
                    productControl.grMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                }
                productControl.tbName.Text = product.PRO_NAME;
                productControl.tbNo.Text = product.PRO_ID;
                productControl.tbYear.Text = product.PRO_YEAR.ToString();
                productControl.tbAmount.Text = (product.STORAGE_NUMBER).ToString();
                parameter.skpProduct.Children.Add(productControl);
                i++;
            }
            numberproduct = i;

        }

        private void AddProduct(Addproduct parameter)
        {
            var type = DataProvider.Ins.DB.TYPEPRODUCTs.Where(x => x.TYPEPRODUCT_NAME == parameter.cbType.Text).FirstOrDefault();
            var producer = DataProvider.Ins.DB.PRODUCERs.Where(x => x.PRODUCER_NAME == parameter.tbProducer.Text).FirstOrDefault();
            numberproduct++;
            string id = "";
            if (numberproduct < 10)
            {
                id += "P00" + numberproduct.ToString();
            }
            else if (numberproduct < 100)
            {
                id += "P0" + numberproduct.ToString();
            }
            else if (numberproduct < 1000)
            {
                id += "P" + numberproduct.ToString();
            }
            PRODUCT ProductInfo = new PRODUCT();
            ProductInfo.PRO_ID = id;
            ProductInfo.MAXPOWER = int.Parse(parameter.tbMaxPower.Text);
            ProductInfo.DISPLACEMENT = int.Parse(parameter.tbDispla.Text);
            ProductInfo.MAXSPEED = int.Parse(parameter.tbMaxSpeed.Text);
            ProductInfo.ACCELERATION = parameter.tbAcce.Text;
            ProductInfo.TRACTION = parameter.tbTraction.Text;
            ProductInfo.PRICE = decimal.Parse(parameter.tbPrice.Text);
            ProductInfo.PRODUCER_ID = producer.PRODUCER_ID;
            ProductInfo.TYPEPRO_ID = type.TYPEPRODUCT_ID;
            ProductInfo.ENGINELAYOUT = parameter.tbEngine.Text;
            ProductInfo.PRO_NAME = parameter.tbName.Text;
            ProductInfo.STORAGE_NUMBER = 0;
            ProductInfo.SELL_NUMBER = 0;
            ProductInfo.PRO_YEAR = int.Parse(parameter.cbYear.Text);
            if (imagefilename != null)
                ProductInfo.IMG = Converter.Instance.StreamFile(imagefilename);

            try
            {
                DataProvider.Ins.DB.PRODUCTs.Add(ProductInfo);
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            ShowLoadProduct(productPG);
            notifier.ShowSuccess("Add product Successfully");
            parameter.Close();
        }

        private void UpLoadIMGEdit(Grid parameter)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == DialogResult.OK == true)
            {
                imagefilename = op.FileName;
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(imagefilename);
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                parameter.Background = imageBrush;
            }
        }

        private void UpdateProduct(EditProduct parameter)
        {
            var ProductInfo = DataProvider.Ins.DB.PRODUCTs.Find(idProduct);
            ProductInfo.MAXPOWER = int.Parse(parameter.tbMaxPower.Text);
            ProductInfo.DISPLACEMENT = int.Parse(parameter.tbDispla.Text);
            ProductInfo.MAXSPEED = int.Parse(parameter.tbMaxSpeed.Text);
            ProductInfo.ACCELERATION = parameter.tbAcce.Text;
            ProductInfo.TRACTION = parameter.tbTraction.Text;
            ProductInfo.PRICE = decimal.Parse(parameter.tbPrice.Text);
            var id = DataProvider.Ins.DB.PRODUCERs.Where(x => x.PRODUCER_NAME == parameter.cbProducer.Text).FirstOrDefault();
            ProductInfo.PRODUCER_ID = id.PRODUCER_ID;
            var idtype = DataProvider.Ins.DB.TYPEPRODUCTs.Where(x => x.TYPEPRODUCT_NAME == parameter.cbType.Text).FirstOrDefault();
            ProductInfo.TYPEPRODUCT = idtype;
            ProductInfo.ENGINELAYOUT = parameter.tbEngine.Text;
            ProductInfo.PRO_NAME = parameter.tbName.Text;
            ProductInfo.PRO_YEAR = int.Parse(parameter.cbYear.Text);
            if (imagefilename != null)
                ProductInfo.IMG = Converter.Instance.StreamFile(imagefilename);
            DataProvider.Ins.DB.SaveChanges();
            ShowLoadProduct(productPG);
            notifier.ShowSuccess("Update Product Successfully");
            parameter.Close();


        }

        private void ShowAddProduct(ProductPG parameter)
        {
            Addproduct addproduct = new Addproduct();
            addproduct.Title = "Add Product";
            int year = DateTime.Now.Year;
            for (int i = 1800; i <= year; i++)
                addproduct.cbYear.Items.Add(i);
            var listProducer = DataProvider.Ins.DB.PRODUCERs.ToList();
            var listtype = DataProvider.Ins.DB.TYPEPRODUCTs.ToList();
            foreach (var item in listProducer)
                addproduct.tbProducer.Items.Add(item.PRODUCER_NAME);
            foreach (var item in listtype)
                addproduct.cbType.Items.Add(item.TYPEPRODUCT_NAME);
            addproduct.ShowDialog();

        }

        private void ShowLoadProduct(ProductPG parameter)
        {
            this.productPG = parameter;
            if (AccountInfo.Type_User == 1)
            {
                parameter.btnAddProduct.Visibility = Visibility.Collapsed;
                parameter.btnListProducer.Visibility = Visibility.Collapsed;
            }
            else
            { 
                parameter.btnAddProduct.Visibility = Visibility.Visible;
                parameter.btnListProducer.Visibility = Visibility.Visible;
                    }
            parameter.skpProduct.Children.Clear();
            var listProduct = DataProvider.Ins.DB.PRODUCTs.ToList();
            bool flat = false;
            int i = 0;
            foreach (var product in listProduct)
            {
                ProductControl productControl = new ProductControl();
                flat = !flat;
                if (AccountInfo.Type_User == 1)
                {
                    productControl.btneditinfoproduct.Visibility = Visibility.Collapsed;
                }
                else
                {
                    productControl.btneditinfoproduct.Visibility = Visibility.Visible;
                }
                productControl.tbName.Text = product.PRO_NAME;
                productControl.tbNo.Text = product.PRO_ID;
                productControl.tbType.Text = product.TYPEPRODUCT.TYPEPRODUCT_NAME.ToString();
                productControl.tbYear.Text = product.PRO_YEAR.ToString();
                parameter.skpProduct.Children.Add(productControl);
                i++;
            }
            numberproduct = i;
        }

        private void ClickEditProduct(ProductControl parameter)
        {
            this.productControl = parameter;
            idProduct = parameter.tbNo.Text;
            ShowEditProduct(idProduct);
        }

        private void ShowEditProduct(string idProduct)
        {
            EditProduct editProduct = new EditProduct();
            editProduct.Title = "Edit the information of Car";
            var productInfo = DataProvider.Ins.DB.PRODUCTs.Find(idProduct);
            int year = DateTime.Now.Year;
            for (int i = 1800; i <= year; i++)
                editProduct.cbYear.Items.Add(i);
            editProduct.cbYear.Text = productInfo.PRO_YEAR.ToString();
            editProduct.tbName.Text = productInfo.PRO_NAME;
            var listproducer = DataProvider.Ins.DB.PRODUCERs.ToList();
            foreach(var producer in listproducer)
            {
                editProduct.cbProducer.Items.Add(producer.PRODUCER_NAME);
            }
            var listproducttype = DataProvider.Ins.DB.TYPEPRODUCTs.ToList();
            foreach(var type in listproducttype)
            {
                editProduct.cbType.Items.Add(type.TYPEPRODUCT_NAME);
            }
            editProduct.cbType.SelectedIndex = editProduct.cbType.Items.IndexOf(productInfo.TYPEPRODUCT.TYPEPRODUCT_NAME);
            editProduct.cbProducer.SelectedIndex =  editProduct.cbProducer.Items.IndexOf(productInfo.PRODUCER.PRODUCER_NAME);
            editProduct.tbEngine.Text = productInfo.ENGINELAYOUT;
            editProduct.tbDispla.Text = productInfo.DISPLACEMENT.ToString();
            editProduct.tbMaxSpeed.Text = productInfo.MAXSPEED.ToString();
            editProduct.tbMaxPower.Text = productInfo.MAXPOWER.ToString();
            editProduct.tbAcce.Text = productInfo.ACCELERATION;
            editProduct.tbTraction.Text = productInfo.TRACTION;
            if(AccountInfo.Type_User == 1)
            {
                editProduct.tbPrice.IsEnabled = false;
            }
            else
            {
                editProduct.tbPrice.IsEnabled = true;
            }
            editProduct.tbPrice.Text = ((int)productInfo.PRICE).ToString();
            if (productInfo.IMG != null)
            {
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = Converter.Instance.ToImage(productInfo.IMG);
                editProduct.grdSelectImage.Background = imageBrush;
            }
            editProduct.ShowDialog();
        }
    }
}