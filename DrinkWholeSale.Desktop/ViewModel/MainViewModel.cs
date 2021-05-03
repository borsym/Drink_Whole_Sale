using DrinkWholeSale.Desktop.Model;
using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace DrinkWholeSale.Desktop.ViewModel
{
    /*IDE MÉG KELL A PORDUCT*/
    public class MainViewModel : ViewModelBase
    {
        private readonly DrinkWholeSaleApiService _service;
        private ObservableCollection<MainCatDto> _maincats;

        public ObservableCollection<MainCatDto> Lists
        {
            get { return _maincats; }
            set { _maincats = value; OnPropertyChanged(); }
        }

        private ObservableCollection<SubCatDto> _subcats;
        private ObservableCollection<ProductDto> _products;

        public ObservableCollection<SubCatDto> SubCats
        {
            get { return _subcats; }
            set { _subcats = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ProductDto> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(); }
        }
        public DelegateCommand RefreshListsCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }
        public DelegateCommand SelectCommand { get; private set; }
        public DelegateCommand SelectCommand2 { get; private set; } // ez így??
        public event EventHandler LogoutSucceeded;

        public MainViewModel(DrinkWholeSaleApiService service)
        {
            _service = service;

            RefreshListsCommand = new DelegateCommand(_ => LoadMainCatsAsync());
            LogoutCommand = new DelegateCommand(_ => LogoutAsync());
            SelectCommand = new DelegateCommand(param => LoadSubCatsAsync(param as MainCatDto));
            SelectCommand2 = new DelegateCommand(param => LoadProductAsync(param as SubCatDto));
        }

        private async void LogoutAsync()
        {
            try
            {
               await _service.LogoutAsync();
               LogoutSucceeded?.Invoke(this, EventArgs.Empty);
                
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
           
        }

        private async void LoadMainCatsAsync()
        {
            try
            {
                Lists = new ObservableCollection<MainCatDto>(await _service.LoadMainCatsAsync());
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        private async void LoadSubCatsAsync(MainCatDto maincatDto)
        {
            if (maincatDto is null)
                return;

            try
            {
                SubCats = new ObservableCollection<SubCatDto>(await _service.LoadSubCatsAsync(maincatDto.Id));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void LoadProductAsync(SubCatDto subcatDto)
        {
            if (subcatDto is null)
                return;

            try
            {
                Products = new ObservableCollection<ProductDto>(await _service.LoadProductAsync(subcatDto.Id));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
    }
}
