using DrinkWholeSale.Persistence.DTO;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<MainCatDto>> LoadMainCatsAsync()
        {
            var response = await _client.GetAsync("api/MainCats");  // jol írtam?

            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<MainCatDto>>();
            }

            throw new NetworkException("Service returned respsone: "+ response.StatusCode);
        }

        public async Task<IEnumerable<SubCatDto>> LoadSubCatsAsync(int subcatId)
        {
            var response = await _client.GetAsync($"api/SubCats/{subcatId}");  // jol írtam?

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<SubCatDto>>();
            }

            throw new NetworkException("Service returned respsone: " + response.StatusCode);
        }
        public async Task<IEnumerable<ProductDto>> LoadProductAsync(int productId)
        {
            var response = await _client.GetAsync($"api/Products/{productId}");  // jol írtam?

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<ProductDto>>();
            }

            throw new NetworkException("Service returned respsone: " + response.StatusCode);
        }

    }
}
