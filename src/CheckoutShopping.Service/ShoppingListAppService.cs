using AutoMapper;
using CheckoutShopping.Infrastructure.Interfaces;
using CheckoutShopping.Service.Interfaces;
using CheckoutShopping.Service.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CheckoutShopping.Service
{
    public class ShoppingListAppService: IShoppingListAppService
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ShoppingListAppService> _logger;
        public ShoppingListAppService(IShoppingListRepository shoppingListRepository, IMapper mapper, ILogger<ShoppingListAppService> logger)
        {
            _shoppingListRepository = shoppingListRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddAsync(ShoppingItem shoppingItem)
        {
            _logger.LogInformation("Calling AddAsync method in ShoppingListAppService with shoppingItem: {@0}", shoppingItem);
            await _shoppingListRepository.AddAsync(_mapper.Map<Infrastructure.Models.ShoppingItem>(shoppingItem));
        }

        public async Task<ShoppingItem> GetByProductNameAsync(string name)
        {
            _logger.LogInformation("Calling GetByProductNameAsync method in ShoppingListAppService with name: {0}", name);
            return _mapper.Map<ShoppingItem>(await _shoppingListRepository.GetByProductNameAsync(name));
        }

        public async Task RemoveAsync(int shoppingItemId)
        {
            _logger.LogInformation("Calling RemoveAsync method in ShoppingListAppService with ShoppingItemId: {0}", shoppingItemId);
            await _shoppingListRepository.RemoveAsync(shoppingItemId);
        }

        public async Task<ShoppingItem> UpdateAsync(ShoppingItem shoppingItem)
        {
            _logger.LogInformation("Calling UpdateAsync method in ShoppingListAppService with shoppingItem: {@0}", shoppingItem);
            var repoItem = _mapper.Map<Infrastructure.Models.ShoppingItem>(shoppingItem);
            return _mapper.Map<ShoppingItem>(await _shoppingListRepository.UpdateAsync(repoItem));
        }

        public async Task<IEnumerable<ShoppingItem>> GetAsync()
        {
            _logger.LogInformation("Calling GetAsync method in ShoppingListAppService");
            return _mapper.Map<IEnumerable<ShoppingItem>>(await _shoppingListRepository.GetAsync());
        }
    }
}
