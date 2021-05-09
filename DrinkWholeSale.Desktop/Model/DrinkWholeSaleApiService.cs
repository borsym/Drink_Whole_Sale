using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrinkWholeSale.Desktop.Model
{
    public class DrinkWholeSaleApiService
    {
        private HttpClient _client;

        public DrinkWholeSaleApiService(string baseAddress)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            LoginDto user = new LoginDto
            {
                UserName = userName,
                Password = password
            };
            var response = await _client.PostAsJsonAsync("api/Account/Login", user);
            if (response.IsSuccessStatusCode)
                return true;
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return false;
            throw new NetworkException("Service returned response: " + response.StatusCode);
        }
        /*
         Ide még lehet fel kell venni 2 új dolgot (subcathez ugyan ez a kezdő metódus
         */
        public async Task LogoutAsync()
        {
            var response = await _client.PostAsync("api/Account/Logout" ,null);  // jol írtam?

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new NetworkException("Service returned respsone: " + response.StatusCode);
        }

        public async Task<IEnumerable<MainCatDto>> LoadMainCatsAsync()
        {
            var response = await _client.GetAsync("api/MainCats");  // jol írtam?

            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<MainCatDto>>();
            }

            throw new NetworkException("Service returned respsone: "+ response.StatusCode);
        }
        public async Task<IEnumerable<OrderDto>> LoadOrdersAsync()
        {
            var response = await _client.GetAsync("api/Orders");  // jol írtam?

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<OrderDto>>();
            }

            throw new NetworkException("Service returned respsone: " + response.StatusCode);
        }
        

        public async Task<IEnumerable<SubCatDto>> LoadSubCatsAsync(int subcatId)
        {
            var response = await _client.GetAsync($"api/SubCats/MainCat/{subcatId}");  // jol írtam?

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<SubCatDto>>();
            }

            throw new NetworkException("Service returned respsone: " + response.StatusCode);
        }
        public async Task<IEnumerable<ProductDto>> LoadProductAsync(int productId)
        {
            var response = await _client.GetAsync($"api/Products/SubCat/{productId}");  // jol írtam?

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<ProductDto>>();
            }

            throw new NetworkException("Service returned respsone: " + response.StatusCode);
        }
        

        public async Task<IEnumerable<OrderDto>> LoadOrderAsync(int orderId)
        {
            var response = await _client.GetAsync($"api/Orders/{orderId}");  // jol írtam?

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<OrderDto>>();
            }

            throw new NetworkException("Service returned respsone: " + response.StatusCode);
        }
        public async Task CreateMainCatAsync(MainCatDto list)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/MainCats/", list);
            list.Id = (await response.Content.ReadAsAsync<MainCatDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateMainCatAsync(MainCatDto list)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/MainCats/{list.Id}", list);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task DeleteMainCatAsync(Int32 listId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/MainCats/{listId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }


        public async Task CreateSubCatAsync(SubCatDto list)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/SubCats/", list);
            list.Id = (await response.Content.ReadAsAsync<MainCatDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }
        // ITT IS LEHET BAJ
        public async Task UpdateSubCatAsync(SubCatDto list)
        {
            Debug.WriteLine("ITT VAN:: "+list.Id);
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/SubCats/{list.Id}", list);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task DeleteSubCatAsync(Int32 listId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/SubCats/{listId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }


        public async Task CreateProductAsync(ProductDto list)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Products/", list);
            list.Id = (await response.Content.ReadAsAsync<MainCatDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }
        // ITT BAJ LEHET, lehet kisbeítű mindenhol a api/eza rész
        public async Task UpdateProductAsync(ProductDto list)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/Products/{list.Id}", list);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task DeleteProductAsync(Int32 listId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/Products/{listId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

    }
}
