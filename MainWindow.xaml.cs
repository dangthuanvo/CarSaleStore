        using CarSalesSystem.General;
using CarSalesSystem.ViewModel;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace CarSalesSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isMaximized = false;
        public MainWindowViewModel mainwindow;
        public MainWindow()
        {
            InitializeComponent();
            homeBtn_Click(this, null);
            this.DataContext = mainwindow = new MainWindowViewModel();

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

        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void BorderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Admin/Pages/Dashboard.xaml", UriKind.RelativeOrAbsolute));
           
        }

        private void employeeBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Admin/Pages/Employee.xaml", UriKind.RelativeOrAbsolute));
   
        }

        private void customerBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Admin/Pages/Customer.xaml", UriKind.RelativeOrAbsolute));
        }

        private void productBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Admin/Pages/Product.xaml", UriKind.RelativeOrAbsolute));
        }

        private void warehouseBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Admin/Pages/Warehouse.xaml", UriKind.RelativeOrAbsolute));
        }

        private void infoBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Admin/Pages/Info.xaml", UriKind.RelativeOrAbsolute));
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult d = (DialogResult)System.Windows.MessageBox.Show("Do you want to sign out?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (d == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
                new SignIn().ShowDialog();
            }
            else return; ;

        }

        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Admin/Pages/Order.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
