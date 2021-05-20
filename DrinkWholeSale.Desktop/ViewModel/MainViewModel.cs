using DrinkWholeSale.Desktop.Model;
using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrinkWholeSale.Desktop.ViewModel
{
    /*IDE MÉG KELL A PORDUCT*/
    public class MainViewModel : ViewModelBase
    {
        private readonly DrinkWholeSaleApiService _service;
        private ObservableCollection<MainCatViewModel> _maincats;
        private ObservableCollection<SubCatViewModel> _subcats;
        private ObservableCollection<ProductViewModel> _products;
        private ObservableCollection<OrderViewModel> _order;
        private ObservableCollection<String> _packing;
        private ObservableCollection<ShoppingCartDto> _ordersProduct;

        private ObservableCollection<OrderViewModel> tmpOrders;

        private SubCatViewModel _selectedSubCat;
        private MainCatViewModel _selectedMainCat;
        private SubCatViewModel _editableSubCat;
        private OrderViewModel _selectedOrder;
        private String _selectedOrderName; 
        private String _selectedMainCatName;

        private ProductViewModel _editableProduct;
        private ProductViewModel _selectedProduct;
        private String _selectedSubCatName;

        private String _filterName;
        private bool _filterFullFilled;
        private DateTime _filterDate;

        public DateTime FilterDate
        {
            get { return _filterDate; }
            set { _filterDate = value; OnPropertyChanged(); }
        }

        public String FilterName
        {
            get { return _filterName; }
            set { _filterName = value; OnPropertyChanged(); }
        }

        public bool FilterFullFilled
        {
            get { return _filterFullFilled; }
            set { _filterFullFilled = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ShoppingCartDto> OrdersProduct
        {
            get { return _ordersProduct; }
            set { _ordersProduct = value; OnPropertyChanged(); }
        }
        public ObservableCollection<String> Packing {
            get { return _packing; }
            set { _packing = value; OnPropertyChanged(); }
        }
        public ObservableCollection<MainCatViewModel> MainCats
        {
            get { return _maincats; }
            set { _maincats = value; OnPropertyChanged(); }
        }
        public ObservableCollection<OrderViewModel> Orders
        {
            get { return _order; }
            set { _order = value; OnPropertyChanged(); }
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
        public DelegateCommand RefreshOrderCommand { get; private set; }
        public DelegateCommand FilterCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }
        public DelegateCommand OrderCommand { get; private set; }
        public DelegateCommand AcceptCommand { get; private set; }
        public DelegateCommand SelectCommand { get; private set; }
        public DelegateCommand SelectCommand2 { get; private set; } 
        
        public DelegateCommand SelectCommandOrder { get; private set; } 
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


        public DelegateCommand SaveFilterCommand { get; private set; }
        public DelegateCommand CancelFilterCommand { get; private set; }
        public DelegateCommand ClearFilterCommand { get; private set; }

        public DelegateCommand ChangeImageCommand { get; private set; }
        public SubCatViewModel SelectedSubCat { get { return _selectedSubCat; } set { _selectedSubCat = value; OnPropertyChanged(); } }

        public MainCatViewModel SelectedMainCat { get { return _selectedMainCat; } set { _selectedMainCat = value; OnPropertyChanged(); } }
        
        public ProductViewModel SelectedProduct { get { return _selectedProduct; } set { _selectedProduct = value; OnPropertyChanged(); } }
        public OrderViewModel SelectedOrder { get { return _selectedOrder; } set { _selectedOrder = value; OnPropertyChanged(); } }

        public string SelectedMainCatName { get { return _selectedMainCatName; } set { _selectedMainCatName = value; OnPropertyChanged(); } }
        public string SelectedSubCatName { get { return _selectedSubCatName; } set { _selectedSubCatName = value; OnPropertyChanged(); } }
        public string SelectedOrderName { get { return _selectedOrderName; } set { _selectedOrderName = value; OnPropertyChanged(); } }

        

        public event EventHandler LogoutSucceeded;
        public event EventHandler StartingSubCatEdit;
        public event EventHandler StartingProductEdit;
        public event EventHandler StartingFilter;

        public event EventHandler FinishingSubCatEdit;
        public event EventHandler FinishingProductEdit;
        public event EventHandler FinishingFilter;

        public event EventHandler StartingImageChange;
        public event EventHandler OpenOrders;
        public event EventHandler OpenFilterWindow;
        public MainViewModel(DrinkWholeSaleApiService service)
        {
            _service = service;

            RefreshListsCommand = new DelegateCommand(_ => LoadMainCatsAsync());
            RefreshOrderCommand = new DelegateCommand(_ => LoadOrdersAsync());
            OrderCommand = new DelegateCommand(_ => LoadOrdersAsync(true));
            LogoutCommand = new DelegateCommand(_ => LogoutAsync());
            FilterCommand = new DelegateCommand(_ => OpenFilter());

            List<Packaging> packagings = new List<Packaging>(Enum.GetValues(typeof(Packaging)).Cast<Packaging>());
            _packing = new ObservableCollection<String>();
            foreach(Packaging p in packagings)
            {
                _packing.Add(p.ToString());
            }
            SelectCommand2 = new DelegateCommand(param => LoadProductAsync(SelectedSubCat));
            SelectCommand = new DelegateCommand(param => LoadSubCatsAsync(SelectedMainCat));
            SelectCommandOrder = new DelegateCommand(param => LoadOrderAsync(SelectedOrder));
            AcceptCommand = new DelegateCommand(_ => !(SelectedOrder is null), _ => ChangeStateOfOrderAsync());
            AddMainCatCommand = new DelegateCommand(_ => AddMainCat());
            EditMainCatCommand = new DelegateCommand(_ => !(SelectedMainCat is null), _ => EditMainCat());
            DeleteMainCatCommand = new DelegateCommand(_ => !(SelectedMainCat is null), _ => DeleteMainCat(SelectedMainCat));

            AddSubCatCommand = new DelegateCommand(_ => !(SelectedMainCat is null), _ => AddSubCat());
            EditSubCatCommand = new DelegateCommand(_ => !(SelectedSubCat is null), _ => StartEditSubCat());
            DeleteSubCatCommand = new DelegateCommand(_ => !(SelectedSubCat is null), _ => DeleteSubCat(SelectedSubCat));

            SaveSubCatEditCommand = new DelegateCommand(_ => SaveSubCatEdit());
            CancelSubCatEditCommand = new DelegateCommand(_ => CancelSubCatEdit());


            AddProductCommand = new DelegateCommand(_ => AddProduct());
            EditProductCommand = new DelegateCommand(_ => !(SelectedProduct is null), _ => StartEditProduct());
            DeleteProductCommand = new DelegateCommand(_ => !(SelectedProduct is null), _ => DeleteProduct(SelectedProduct));

            SaveProductEditCommand = new DelegateCommand(_ => SaveProductEdit());
            CancelProductEditCommand = new DelegateCommand(_ => CancelItemEdit());

            SaveFilterCommand = new DelegateCommand(_ => FilterOrders());
            ClearFilterCommand = new DelegateCommand(_ => ClearFilters());

            ChangeImageCommand = new DelegateCommand(_ => StartingImageChange?.Invoke(this, EventArgs.Empty));
            SubCats = new ObservableCollection<SubCatViewModel>();

        }

        private void ClearFilters()
        {
            FilterName = "";
            LoadOrdersAsync();
            FinishingFilter?.Invoke(this, EventArgs.Empty);
        }

        private void OpenFilter()
        {
            if(!(OrdersProduct is null))
                OrdersProduct.Clear();
            OpenFilterWindow?.Invoke(this, EventArgs.Empty);
            
        }

        public void FilterOrders()
        {

            if (FilterName != null)
            {
                Orders = new ObservableCollection<OrderViewModel>(Orders.Where(t => t.Name.StartsWith(FilterName)).ToList());
            }
            if (FilterDate != null)
            {
                Orders = new ObservableCollection<OrderViewModel>(Orders.Where(t => t.OrderDate.Date == FilterDate.Date).ToList());
            }
            if (FilterFullFilled)
            {
                Orders = new ObservableCollection<OrderViewModel>(Orders.Where(t => t.Fulfilled == true).ToList());
            }
            else
            {
                Orders = new ObservableCollection<OrderViewModel>(Orders.Where(t => t.Fulfilled == false || t.Fulfilled == true).ToList());
            }
            


            FinishingFilter?.Invoke(this, EventArgs.Empty);
        }

        private async Task ChangeStateOfOrderAsync()
        {
            try
            {
                var order = (OrderDto)SelectedOrder;
                order.Items = OrdersProduct.ToList();
                if (await _service.FullFillOrders(order))
                {
                    LoadOrdersAsync();
                    if(SelectedSubCat != null)
                    {
                        LoadProductAsync(SelectedSubCat);
                    }
                }
            }

            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
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

        private async void LoadOrdersAsync(bool open = false)
        {
            try
            {
                Orders = new ObservableCollection<OrderViewModel>((await _service.LoadOrdersAsync())
                   .Select(list => (OrderViewModel)list));

                foreach(OrderViewModel o in Orders)
                {
                    o.FulfilledText = (o.Fulfilled) ? "Full filled" : "Not full filled";
                }
                tmpOrders = Orders;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            if(open) OpenOrders?.Invoke(this, EventArgs.Empty);
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
        

        private async void LoadOrderAsync(OrderViewModel subcatDto)
        {
            if (subcatDto is null)
                return;

            try
            {
                SelectedOrderName = subcatDto.Name;
                OrdersProduct = new ObservableCollection<ShoppingCartDto>((await _service.LoadOrderAsync(subcatDto.Id))
                    .Select(item => (ShoppingCartDto)item));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            int a = 0;
        }
        

        private async void AddProduct()
        {
            var newItem = new ProductViewModel
            {
                Name = "New Item",
                SubCatId = SelectedSubCat.Id,
                Producer = "No one",
                Description = ""
                
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
            
            LoadProductAsync(SelectedSubCat);
            FinishingProductEdit?.Invoke(this, EventArgs.Empty);
        }
    }
}
