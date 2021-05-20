using DrinkWholeSale.Desktop.Model;
using DrinkWholeSale.Desktop.View;
using DrinkWholeSale.Desktop.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
        private SubCatEditorWindow _subCatEditView;
        private ProductEditorWindow _productEditView;
        private OrderWindow _orderWindow;
        private FilterWindow _filterWindow;
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
            _mainViewModel.LogoutSucceeded += _mainViewModel_LogutSucceeded;
            _mainViewModel.MessageApplication += _mainViewModel_Message;

            _mainViewModel.StartingSubCatEdit += _mainViewModel_StartingSubCatEdit;
            _mainViewModel.FinishingSubCatEdit += _mainViewModel_FinishingSubCatEdit;
            
            _mainViewModel.StartingProductEdit += _mainViewModel_StartingProductEdit;
            _mainViewModel.FinishingProductEdit += _mainViewModel_FinishingProductEdit;
            _mainViewModel.StartingImageChange += _mainViewModel_StartingImageChange;
            _mainViewModel.OpenOrders += _mainViewModel_OrdersTab;
            _mainViewModel.FinishingFilter += _mainViewModel_CloseFilter;
            _mainViewModel.OpenFilterWindow += _mainViewModel_OpenFilter;
            _view = new MainWindow
            {
                DataContext = _mainViewModel
            };

            

            _loginView.Show();
            
        }

        private void _mainViewModel_CloseFilter(object sender, EventArgs e) // itt baj van
        {
            if (_filterWindow.IsActive)
            {
                _filterWindow.Close();
            }
        }

        private void _mainViewModel_OpenFilter(object sender, EventArgs e)
        {
            _filterWindow = new FilterWindow
            {
                DataContext = _mainViewModel
            };
            _filterWindow.ShowDialog();
        }

        private void _mainViewModel_OrdersTab(object sender, EventArgs e)
        {
            _orderWindow = new OrderWindow
            {
                DataContext = _mainViewModel
            };
            _orderWindow.Show();
        }

        private async void _mainViewModel_StartingImageChange(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Images|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png;",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dialog.ShowDialog(_subCatEditView).GetValueOrDefault(false))
            {
                _mainViewModel.EditableProduct.Image = await File.ReadAllBytesAsync(dialog.FileName);
            }
        }
        
        private void _mainViewModel_FinishingProductEdit(object sender, EventArgs e) // itt baj van
        {
            if (_productEditView.IsActive)
            {
                _productEditView.Close();
            }
        }

        private void _mainViewModel_StartingProductEdit(object sender, EventArgs e)
        {
            _productEditView = new ProductEditorWindow
            {
                DataContext = _mainViewModel
            };
            _productEditView.ShowDialog();
        }

        private void _mainViewModel_FinishingSubCatEdit(object sender, EventArgs e)
        {
            if (_subCatEditView.IsActive)
            {
                _subCatEditView.Close();
            }
        }

        private void _mainViewModel_StartingSubCatEdit(object sender, EventArgs e)
        {
            _subCatEditView = new SubCatEditorWindow
            {
                DataContext = _mainViewModel
            };
            _subCatEditView.ShowDialog();
        }

        private void _mainViewModel_LogutSucceeded(object sender, EventArgs e)
        {
            _view.Hide();
            _loginView.Show();
        }
        private void _loginViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            _loginView.Hide();
            
            //_orderWindow.ShowDialog();
            _view.Show();
        }


        private void _loginViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Login failed!", "DrinkWholeSale", MessageBoxButton.OK);
        }

    

        private void _mainViewModel_Message(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "DrinkWholeSale", MessageBoxButton.OK);
        }
    }
}
