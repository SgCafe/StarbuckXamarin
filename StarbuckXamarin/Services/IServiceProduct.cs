using StarbuckXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckXamarin.Services
{
    public interface IServiceProduct
    {
        Task<List<Product>> GetProductAsync();
        Task<bool> ChangeFavoriteItem(string nameProd, bool favProd);
        Task<List<Product>> FilterFavItems();
        Task<bool> SendCartItems(List<Cart> cartItem);
        Task<List<Cart>> GetItemsCart();

    }
}
