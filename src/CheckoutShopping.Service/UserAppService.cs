using CheckoutShopping.Core.Auth;
using CheckoutShopping.Service.Interfaces;
using Microsoft.Extensions.Options;

namespace CheckoutShopping.Service
{
    public class UserAppService : IUserAppService
    {
        private readonly Credentials _credentials;
       
        public UserAppService(IOptions<Credentials> credentials)
        {
            _credentials = credentials.Value;
        }
        public bool Login(string userName, string password)
        {
            if (userName == _credentials.UserName && password == _credentials.Password) return true;
            return false;
        }
    }
}
