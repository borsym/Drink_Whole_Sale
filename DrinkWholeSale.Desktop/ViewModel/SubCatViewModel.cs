using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Desktop.ViewModel
{
    class SubCatViewModel : ViewModelBase
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

        private int _mainCatId;

        public int MainCatId
        {
            get { return _mainCatId; }
            set { _mainCatId = value; OnPropertyChanged(); }
        }

        public SubCatViewModel ShallowClone()
        {
            return (SubCatViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(SubCatViewModel rhs)
        {
            Id = rhs.Id;
            Name = rhs.Name;
         
        }

        public static explicit operator SubCatViewModel(SubCatDto dto) => new SubCatViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
           
        };

        public static explicit operator SubCatDto(SubCatViewModel vm) => new SubCatDto
        {
            Id = vm.Id,
            Name = vm.Name,
           
        };
    }
}
