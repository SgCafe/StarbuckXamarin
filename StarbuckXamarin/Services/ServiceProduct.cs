using Firebase.Database;
using Firebase.Database.Query;
using StarbuckXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckXamarin.Services
{
    public class ServiceProduct : IServiceProduct
    {
        protected static string PasswordFirebase = "K9H8GbGsIUTQT2pP5l860ZNZnZTuEKD7tcNtLpkd";
        private readonly FirebaseClient Client = new FirebaseClient("https://starbucksxamarin-default-rtdb.firebaseio.com/",
            new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(PasswordFirebase) });

        public async Task<List<Product>> GetProductAsync()
        {
            try
            {
                var specialItems = await GetItemsFromCategory("Especiais");
                var expressItems = await GetItemsFromCategory("Expressos e Cafés");
                var frappuccinoItems = await GetItemsFromCategory("Frappuccino");

                var combinedItems = specialItems.Concat(expressItems).Concat(frappuccinoItems).ToList();

                return combinedItems;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Product>> GetItemsFromCategory(string category)
        {
            var items = await Client
                .Child("Cardapio")
                .Child(category)
                .OnceAsync<Product>();


            return items.Select(i => new Product
            {
                CategoryName = i.Object.CategoryName,
                Name = i.Object.Name,
                Image = i.Object.Image,
                ValueTall = i.Object.ValueTall,
                Grande = i.Object.Grande,
                Venti = i.Object.Venti,
                Quality = i.Object.Quality,
                ProductFavItem = i.Object.ProductFavItem,
            }).ToList();
        }
    }
}
