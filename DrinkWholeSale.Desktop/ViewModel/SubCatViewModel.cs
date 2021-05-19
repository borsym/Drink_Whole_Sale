using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Desktop.ViewModel
{
   public class SubCatViewModel : ViewModelBase
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
            MainCatId = rhs.MainCatId;
         
        }

        public static explicit operator SubCatViewModel(SubCatDto dto) => new SubCatViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            MainCatId = dto.MainCatId
           
        };

        public static explicit operator SubCatDto(SubCatViewModel vm) => new SubCatDto
        {
            Id = vm.Id,
            Name = vm.Name,
            MainCatId = vm.MainCatId
        };
    }
}
