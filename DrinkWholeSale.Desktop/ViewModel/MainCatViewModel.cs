using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Desktop.ViewModel
{
    public class MainCatViewModel : ViewModelBase
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



        public static explicit operator MainCatViewModel(MainCatDto dto) => new MainCatViewModel
        {
            Id = dto.Id,
            Name = dto.Name
        };

        public static explicit operator MainCatDto(MainCatViewModel vm) => new MainCatDto
        {
            Id = vm.Id,
            Name = vm.Name
        };
    }
}
