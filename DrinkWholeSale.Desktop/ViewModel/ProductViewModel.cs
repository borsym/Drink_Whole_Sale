using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Desktop.ViewModel
{
    class ProductViewModel : ViewModelBase
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
        }

        public static explicit operator ProductViewModel(ProductDto dto) => new ProductViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
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
