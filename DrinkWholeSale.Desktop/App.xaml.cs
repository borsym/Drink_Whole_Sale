using DrinkWholeSale.Desktop.Model;
using DrinkWholeSale.Desktop.View;
using DrinkWholeSale.Desktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DrinkWholeSale.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DrinkWholeSaleApiService _service;
        private LoginWindow _loginView;
        private MainViewModel _mainViewModel;
        private LoginViewModel _loginViewModel;
        private MainWindow _view;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new DrinkWholeSaleApiService(ConfigurationManager.AppSettings["baseAddress"]);
         
            _loginViewModel = new LoginViewModel(_service);
            _loginViewModel.LoginSucceeded += _loginViewModel_LoginSucceeded;
            _loginViewModel.LoginFailed += _loginViewModel_LoginFailed;
            _loginViewModel.MessageApplication += _mainViewModel_Message;

            _loginView = new LoginWindow
            {
                DataContext = _loginViewModel
            };

            _mainViewModel = new MainViewModel(_service);
            _mainViewModel.MessageApplication += _mainViewModel_Message;

            _view = new MainWindow
            {
                DataContext = _mainViewModel
            };

            _loginView.Show();
        }

       

        private void _loginViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Login failed!", "DrinkWholeSale", MessageBoxButton.OK);
        }

        private void _loginViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            _loginView.Hide();
            _view.Show();
        }

        private void _mainViewModel_Message(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "DrinkWholeSale", MessageBoxButton.OK);
        }
    }
}
