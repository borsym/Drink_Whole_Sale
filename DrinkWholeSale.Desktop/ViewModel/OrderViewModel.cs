using DrinkWholeSale.Desktop.Model;
using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Desktop.ViewModel
{
    public class OrderViewModel : ViewModelBase
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

        private String _email;

        public String Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        private String _phone;

        public String Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(); }
        }
        private String _address;

        public String Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }


        private int _guestId;

        public int GuestId
        {
            get { return _guestId; }
            set { _guestId = value; OnPropertyChanged(); }
        }

        private bool _fulfilled;

        public bool Fulfilled
        {
            get { return _fulfilled; }
            set { _fulfilled = value; OnPropertyChanged(); }
        }

        private String _fulfilledText;
        public String FulfilledText
        {
            get { return _fulfilledText; }
            set { _fulfilledText = value; OnPropertyChanged(); }
        }

        public DateTime _orderDate;
        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; OnPropertyChanged(); }
        }
        public OrderViewModel ShallowClone()
        {
            return (OrderViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(OrderViewModel rhs)
        {
            Id = rhs.Id;
            Name = rhs.Name;
           
        }

        public static explicit operator OrderViewModel(OrderDto dto) => new OrderViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Address = dto.Address,
            Phone = dto.Phone,
            Email = dto.Email,
            GuestId = dto.GuestId,
            Fulfilled = dto.fulfilled,
            OrderDate = dto.orderDate


        };

        public static explicit operator OrderDto(OrderViewModel vm) => new OrderDto
        {
            Id = vm.Id,
            Name = vm.Name,
            Address = vm.Address,
            Phone = vm.Phone,
            Email = vm.Email,
            GuestId = vm.GuestId,
            fulfilled = vm.Fulfilled,
            orderDate = vm.OrderDate
        };
    }
}
