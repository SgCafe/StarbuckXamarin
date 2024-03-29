﻿using Firebase.Database;
using Firebase.Database.Query;
using StarbuckXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public async Task<bool> ChangeFavoriteItem(string nameProd, bool favProd)
        {
            try
            {
                var combinedItems = await GetProductAsync();
                var itemUpdate = combinedItems.FirstOrDefault(i => i.Name == nameProd);

                if (itemUpdate != null)
                {
                    itemUpdate.ProductFavItem = favProd;
                    var category = itemUpdate.CategoryName;
                    var productToUpdate = itemUpdate;

                    await Client
                        .Child("Cardapio")
                        .Child(category)
                        .Child(nameProd)
                        .PutAsync(productToUpdate);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error changeFavoriteItem: {ex.Message}", "ok");
                return false;
            }
        }

        public async Task<List<Product>> FilterFavItems()
        {
            try
            {
                var filteredItems = await GetProductAsync();

                var itemsShow = filteredItems.Where(i => i.ProductFavItem).ToList();

                return itemsShow;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error Filter FAV ITEMS: {ex.Message}", "ok");
                return null;
            }
        }

        public async Task<bool> SendCartItems(List<Cart> cartItem)
        {
            try
            {
                var cartItemRef = Client.Child("Carrinho");

                foreach (var item in cartItem)
                {
                    if (string.IsNullOrEmpty(item.FirebaseKey))
                    {
                        item.FirebaseKey = GenerateCartKey();
                    }

                    var existingItemRef = cartItemRef.Child(item.FirebaseKey);

                    if (existingItemRef != null)
                    {
                        await existingItemRef.PutAsync(item);
                    }
                    else
                    {
                        await cartItemRef.Child(item.FirebaseKey).PutAsync(item);
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error SendCart : {ex.Message}", "ok");

                return false;
            }
        }

        private string GenerateCartKey()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<List<Cart>> GetItemsCart()
        {
            var items = await Client
                .Child("Carrinho")
                .OnceAsync<Cart>();

            return items.Select(c => new Cart
            {
                Name = c.Object.Name,
                Image = c.Object.Image,
                Price = c.Object.Price,
                Size = c.Object.Size,
                Quantity = c.Object.Quantity,
                TotalValue = c.Object.TotalValue,
                FirebaseKey = c.Key,
            }).ToList();
        }

        public async Task<bool> DeleteItemCart(string firebaseKey)
        {
            try
            {
                var key = $"Carrinho/{firebaseKey}";

                await Client
                    .Child(key)
                    .DeleteAsync();

                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting cart item: {ex.Message}");
                return false;
            }
        }

        public async Task CountLessItemCart(string firebaseKey, int count)
        {
            try
            {
                var key = $"Carrinho/{firebaseKey}/Quantity";

                var quantityRef = Client.Child(key);
                await quantityRef.PutAsync(count);

                var itemRef = Client.Child($"Carrinho/{firebaseKey}");
                var snapshot = await itemRef.OnceSingleAsync<Cart>();
                await UpdateTotalValue(firebaseKey, snapshot);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating quantity less of cart item: {ex.Message}");
            }
        }

        private async Task UpdateTotalValue(string firebaseKey, Cart cartItem)
        {
            try
            {
                var key = $"Carrinho/{firebaseKey}/TotalValue";
                var updateValue = Client.Child(key);
                var totalValue = cartItem.Price * cartItem.Quantity;
                await updateValue.PutAsync(totalValue);

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error UPDATE TOTAL VALUE : {ex.Message}", "ok");
            }
        }

        public async Task CountMoreItemCart(string firebaseKey, int count)
        {
            try
            {
                var key = $"Carrinho/{firebaseKey}/Quantity";

                var quantityRef = Client.Child(key);
                await quantityRef.PutAsync(count);

                var itemRef = Client.Child($"Carrinho/{firebaseKey}");
                var snapshot = await itemRef.OnceSingleAsync<Cart>();
                await UpdateTotalValue(firebaseKey, snapshot);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating quantity more of cart item: {ex.Message}");
            }
        }
    }
}
