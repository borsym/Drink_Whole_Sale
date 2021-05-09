using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Desktop.ViewModel
{
    public class ProductViewModel : ViewModelBase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private String _description;

        public String Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private byte[] _image;

        public byte[] Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        private int _subCatId;

        public int SubCatId
        {
            get { return _subCatId; }
            set { _subCatId = value; OnPropertyChanged(); }
        }
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(); }
        }

        private int _netprice;
        public int NetPrice
        {
            get { return _netprice; }
            set { _netprice = value; OnPropertyChanged(); }
        }

        private Packaging _pack;
        public Packaging Pack
        {
            get { return _pack; }
            set { _pack = value; OnPropertyChanged(); }
        }


        private int _typeNumber;
        public int TypeNumber
        {
            get { return _typeNumber; }
            set { _typeNumber = value; OnPropertyChanged(); }
        }

        private string _producer;
        public string Producer
        {
            get { return _producer; }
            set { _producer = value; OnPropertyChanged(); }
        }

        private int _grossPrice;
        public int GrossPrice
        {
            get { return _grossPrice; }
            set { _grossPrice = value; OnPropertyChanged(); }
        }

        public ProductViewModel ShallowClone()
        {
            return (ProductViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(ProductViewModel rhs)
        {
            Id = rhs.Id;
            Name = rhs.Name;
            Description = rhs.Description;
            Image = rhs.Image;
            SubCatId = rhs.SubCatId;
            Producer = rhs.Producer;
            TypeNumber = rhs.TypeNumber;
            NetPrice = rhs.NetPrice;
            Quantity = rhs.Quantity;
            Pack = rhs.Pack;
            GrossPrice = (int)Math.Round(rhs.NetPrice * 1.27);
        }

        public static explicit operator ProductViewModel(ProductDto dto) => new ProductViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Producer = dto.Producer,
            TypeNumber = dto.TypeNumber,
            NetPrice = dto.NetPrice,
            Quantity = dto.Quantity,
            Pack = dto.Pack,
            GrossPrice = (int)Math.Round(dto.NetPrice * 1.27),
            Description = dto.Description,
           Image = dto.Image,
            SubCatId = dto.SubCatId

            
        };

        public static explicit operator ProductDto(ProductViewModel vm) => new ProductDto
        {
            Id = vm.Id,
            Name = vm.Name,
            Description = vm.Description,
            Image = vm.Image,
            SubCatId = vm.SubCatId
        };
    }
}
