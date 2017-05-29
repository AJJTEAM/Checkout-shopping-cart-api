using CheckoutShopping.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckoutShopping.Infrastructure.Interfaces
{
    public interface IShoppingListRepository
    {
        Task<IEnumerable<ShoppingItem>> GetAsync();
        Task<ShoppingItem> GetByProductNameAsync(string name);
        Task AddAsync(ShoppingItem shoppingItem);
        Task<ShoppingItem> UpdateAsync(ShoppingItem shoppingItem);
        Task RemoveAsync(int ShoppingItemId);
      
    }
}
