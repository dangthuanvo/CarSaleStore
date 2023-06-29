using CarSalesSystem.Customer.Pages;
using CarSalesSystem.General;
using CarSalesSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
namespace CarSalesSystem
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        bool isMaximized = false;
        CUSTOMER _customer;
        public CustomerWindow()
        {

            InitializeComponent();
            PagesNavigation.Navigate(new Store(_customer));
        }

        

        public CustomerWindow(CUSTOMER cUSTOMER)
        {
            InitializeComponent();
            _customer = cUSTOMER;
            PagesNavigation.Navigate(new Store(_customer));
        }

        private void StoreBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Store(_customer));
        }

        private void billBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Bill(_customer));
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new InfoCus(_customer));
        }

        private void PagesNavigation_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (isMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1280;
                    this.Height = 780;

                    isMaximized = false;

                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    isMaximized = true;
                }
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            DialogResult d = (DialogResult)System.Windows.MessageBox.Show("Do you want to sign out?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (d == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
                new SignIn().ShowDialog();
            }
            else return;
            
        }

        private void trackingBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Tracking(_customer));
        }
    }
}
