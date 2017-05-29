using System.Threading.Tasks;
using CheckoutShopping.Infrastructure.Interfaces;
using CheckoutShopping.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CheckoutShopping.Infrastructure
{
    public class ShoppingListRepository: IShoppingListRepository
    {
        private readonly CheckoutContext _context;
        private readonly ILogger<ShoppingListRepository> _logger;
        public ShoppingListRepository(CheckoutContext context, ILogger<ShoppingListRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(ShoppingItem shoppingItem)
        {
            try
            {
                _logger.LogInformation("Calling AddAsync method in ShoppingListRepository with shoppingItem: {@0}", shoppingItem);
                await _context.AddAsync(shoppingItem);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
           
        }

        public async Task<IEnumerable<ShoppingItem>> GetAsync()
        {
            try
            {
                _logger.LogInformation("Calling GetAsync method in ShoppingListRepository");
                return await _context.ShoppingItems.Include(p => p.Product).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ShoppingItem> GetByProductNameAsync(string name)
        {
            try
            {
                _logger.LogInformation("Calling GetByProductNameAsync method in ShoppingListRepository with name: {0}", name);
                return await _context.ShoppingItems.Include(p=>p.Product).Where(s=>s.Product.Name==name).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveAsync(int shoppingItemId)
        {
            try
            {
                _logger.LogInformation("Calling RemoveAsync method in ShoppingListRepository with shoppingItemId: {0}", shoppingItemId);
                var shoppingItem = await _context.FindAsync<ShoppingItem>(shoppingItemId);
                _context.ShoppingItems.Remove(shoppingItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task<ShoppingItem> UpdateAsync(ShoppingItem shoppingItem)
        {
            try
            {
                _logger.LogInformation("Calling UpdateAsync method in ShoppingListRepository with shoppingItemId: {@0}", shoppingItem);
                var existingItem = await GetByProductNameAsync(shoppingItem.Product.Name);
                if (existingItem == null) throw new KeyNotFoundException(string.Format("ShoppingItem with product name : {0} not found",shoppingItem.Product.Name));
                //updating only the quantity for now 
                existingItem.Product.Quantity = shoppingItem.Product.Quantity;
                await _context.SaveChangesAsync();
                return await GetByProductNameAsync(shoppingItem.Product.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
