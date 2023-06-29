using CarSalesSystem.Viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarSalesSystem.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand LoadWithTypeCommand { get; set; }
        public MainWindowViewModel()
        {
            LoadWithTypeCommand = new RelayCommand<MainWindow>((parameter) => true, (parameter) => LoadWithType(parameter));
        }

        private void LoadWithType(MainWindow parameter)
        {
            if(AccountInfo.Type_User == 0)
            {
                parameter.employeeBtn.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                parameter.employeeBtn.Visibility=System.Windows.Visibility.Collapsed;
            }
        }
    }
}
