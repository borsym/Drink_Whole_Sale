using DrinkWholeSale.Desktop.Model;
using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace DrinkWholeSale.Desktop.ViewModel
{
    /*IDE MÉG KELL A PORDUCT*/
    public class MainViewModel : ViewModelBase
    {
        private readonly DrinkWholeSaleApiService _service;
        private ObservableCollection<MainCatViewModel> _maincats;
        private ObservableCollection<SubCatViewModel> _subcats;
        private ObservableCollection<ProductViewModel> _products;

        private SubCatViewModel _selectedSubCat;
        private MainCatViewModel _selectedMainCat;
        private SubCatViewModel _editableSubCat;
        private String _selectedMainCatName;

        private ProductViewModel _editableProduct;
        private ProductViewModel _selectedProduct;
        private String _selectedSubCatName;

        public ObservableCollection<MainCatViewModel> MainCats
        {
            get { return _maincats; }
            set { _maincats = value; OnPropertyChanged(); }
        }

    
        public ObservableCollection<SubCatViewModel> SubCats
        {
            get { return _subcats; }
            set { _subcats = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ProductViewModel> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(); }
        }

        public SubCatViewModel EditableSubCat { get { return _editableSubCat; } set { _editableSubCat = value; OnPropertyChanged(); } }
        public ProductViewModel EditableProduct { get { return _editableProduct;  }  set { _editableProduct = value; OnPropertyChanged(); } }
        public DelegateCommand RefreshListsCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }
        public DelegateCommand SelectCommand { get; private set; }
        public DelegateCommand SelectCommand2 { get; private set; } // ez így??

        public DelegateCommand AddMainCatCommand { get; private set; }
        public DelegateCommand EditMainCatCommand { get; private set; }
        public DelegateCommand DeleteMainCatCommand { get; private set; }
        public DelegateCommand AddSubCatCommand { get; private set; }
        public DelegateCommand EditSubCatCommand { get; private set; }
        public DelegateCommand DeleteSubCatCommand { get; private set; }
        public DelegateCommand SaveSubCatEditCommand { get; private set; }
        public DelegateCommand CancelSubCatEditCommand { get; private set; }


        public DelegateCommand AddProductCommand { get; private set; }
        public DelegateCommand EditProductCommand { get; private set; }
        public DelegateCommand DeleteProductCommand { get; private set; }
        public DelegateCommand SaveProductEditCommand { get; private set; }
        public DelegateCommand CancelProductEditCommand { get; private set; }


        public DelegateCommand ChangeImageCommand { get; private set; }
        public SubCatViewModel SelectedSubCat { get { return _selectedSubCat; } set { _selectedSubCat = value; OnPropertyChanged(); } }

        public MainCatViewModel SelectedMainCat { get { return _selectedMainCat; } set { _selectedMainCat = value; OnPropertyChanged(); } }

        public ProductViewModel SelectedProduct { get { return _selectedProduct; } set { _selectedProduct = value; OnPropertyChanged(); } }

        public string SelectedMainCatName { get { return _selectedMainCatName; } set { _selectedMainCatName = value; OnPropertyChanged(); } }
        public string SelectedSubCatName { get { return _selectedSubCatName; } set { _selectedSubCatName = value; OnPropertyChanged(); } }

        public event EventHandler LogoutSucceeded;
        public event EventHandler StartingSubCatEdit;
        public event EventHandler StartingProductEdit;

        public event EventHandler FinishingSubCatEdit;
        public event EventHandler FinishingProductEdit;

        public event EventHandler StartingImageChange;

        public MainViewModel(DrinkWholeSaleApiService service)
        {
            _service = service;

            RefreshListsCommand = new DelegateCommand(_ => LoadMainCatsAsync());
            LogoutCommand = new DelegateCommand(_ => LogoutAsync());


            SelectCommand = new DelegateCommand(param => LoadSubCatsAsync(SelectedMainCat));
            SelectCommand2 = new DelegateCommand(param => LoadProductAsync(SelectedSubCat));


            AddMainCatCommand = new DelegateCommand(_ => AddMainCat());
            EditMainCatCommand = new DelegateCommand(_ => !(SelectedMainCat is null), _ => EditMainCat());
            DeleteMainCatCommand = new DelegateCommand(_ => !(SelectedMainCat is null), _ => DeleteMainCat(SelectedMainCat));

            AddSubCatCommand = new DelegateCommand(_ => !(SelectedMainCat is null), _ => AddSubCat());
            EditSubCatCommand = new DelegateCommand(_ => !(SelectedSubCat is null), _ => StartEditSubCat());
            DeleteSubCatCommand = new DelegateCommand(_ => !(SelectedSubCat is null), _ => DeleteSubCat(SelectedSubCat));

            SaveSubCatEditCommand = new DelegateCommand(_ => SaveSubCatEdit());
            CancelSubCatEditCommand = new DelegateCommand(_ => CancelSubCatEdit());


            AddProductCommand = new DelegateCommand(_ => !(SelectedProduct is null), _ => AddProduct());
            EditProductCommand = new DelegateCommand(_ => !(SelectedProduct is null), _ => StartEditProduct());
            DeleteProductCommand = new DelegateCommand(_ => !(SelectedProduct is null), _ => DeleteProduct(SelectedProduct));

            SaveProductEditCommand = new DelegateCommand(_ => SaveProductEdit());
            CancelProductEditCommand = new DelegateCommand(_ => CancelItemEdit());


            ChangeImageCommand = new DelegateCommand(_ => StartingImageChange?.Invoke(this, EventArgs.Empty));


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
                MainCats = new ObservableCollection<MainCatViewModel>((await _service.LoadMainCatsAsync())
                   .Select(list => (MainCatViewModel)list));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void AddMainCat()
        {
            var newList = new MainCatViewModel
            {
                Name = "New MainCat"
            };

            var listDto = (MainCatDto)newList;

            try
            {
                await _service.CreateMainCatAsync(listDto);
                newList.Id = listDto.Id;
                MainCats.Add(newList);
                SelectedMainCat = newList;
            }

            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void DeleteMainCat(MainCatViewModel list)
        {
            try
            {
                await _service.DeleteMainCatAsync(list.Id);
                MainCats.Remove(SelectedMainCat);
                SelectedMainCat = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        private async void EditMainCat()
        {
            try
            {
                SelectedMainCat.Name = SelectedMainCatName;
                await _service.UpdateMainCatAsync((MainCatDto)SelectedMainCat);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }


        private async void LoadSubCatsAsync(MainCatViewModel maincat)
        {
            if (maincat is null) {
                SubCats = null; // itt lehet productot is nullolni kéne
                return;
            }

            try
            {
                SelectedMainCatName = maincat.Name;
                SubCats = new ObservableCollection<SubCatViewModel>((await _service.LoadSubCatsAsync(maincat.Id))
                    .Select(item => (SubCatViewModel)item));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }


        private async void AddSubCat()
        {
            var newItem = new SubCatViewModel
            {
                Name = "New Item",
                MainCatId = SelectedMainCat.Id
            };

            var itemDto = (SubCatDto)newItem;

            try
            {
                await _service.CreateSubCatAsync(itemDto); // ERROR
                newItem.Id = itemDto.Id;
                SubCats.Add(newItem);
                SelectedSubCat = newItem;
            }

            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        private async void DeleteSubCat(SubCatViewModel item)
        {
            try
            {
                await _service.DeleteSubCatAsync(item.Id);
                SubCats.Remove(SelectedSubCat);
                SelectedSubCat = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        private void StartEditSubCat()
        {
            EditableSubCat = SelectedSubCat.ShallowClone();
            StartingSubCatEdit?.Invoke(this, EventArgs.Empty);
        }
        private void CancelSubCatEdit()
        {
            EditableSubCat = null;
            FinishingSubCatEdit?.Invoke(this, EventArgs.Empty);
        }
        private async void SaveSubCatEdit()
        {
            try
            {
                SelectedSubCat.CopyFrom(EditableSubCat);
                await _service.UpdateSubCatAsync((SubCatDto)SelectedSubCat);
                if (SelectedSubCat.MainCatId != SelectedMainCat.Id)
                {
                    SubCats.Remove(SelectedSubCat);
                    SelectedSubCat = null;
                }
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            FinishingSubCatEdit?.Invoke(this, EventArgs.Empty);
        }

        private async void LoadProductAsync(SubCatViewModel subcatDto)
        {
            if (subcatDto is null)
                return;

            try
            {
                SelectedMainCatName = subcatDto.Name;
                Products = new ObservableCollection<ProductViewModel>((await _service.LoadProductAsync(subcatDto.Id))
                    .Select(item => (ProductViewModel)item));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void AddProduct()
        {
            var newItem = new ProductViewModel
            {
                Name = "New Item",
                SubCatId = SelectedSubCat.Id
            };

            var itemDto = (ProductDto)newItem;

            try
            {
                await _service.CreateProductAsync(itemDto);
                newItem.Id = itemDto.Id;
                Products.Add(newItem);
                SelectedProduct = newItem;
            }

            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        private async void DeleteProduct(ProductViewModel item)
        {
            try
            {
                await _service.DeleteProductAsync(item.Id);
                Products.Remove(SelectedProduct);
                SelectedProduct = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        private void StartEditProduct()
        {
            EditableProduct = SelectedProduct.ShallowClone();
            StartingProductEdit?.Invoke(this, EventArgs.Empty);
        }
        private void CancelItemEdit()
        {
            EditableProduct = null;
            FinishingProductEdit?.Invoke(this, EventArgs.Empty);
        }
        private async void SaveProductEdit()
        {
            try
            {
                SelectedProduct.CopyFrom(EditableProduct);
                await _service.UpdateProductAsync((ProductDto)SelectedProduct);
                if (SelectedProduct.SubCatId != SelectedSubCat.Id)
                {
                    Products.Remove(SelectedProduct);
                    SelectedProduct = null;
                }
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            FinishingProductEdit?.Invoke(this, EventArgs.Empty);
        }
    }
}
