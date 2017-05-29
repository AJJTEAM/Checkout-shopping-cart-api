using System.Threading.Tasks;

namespace CheckoutShopping.Service.Interfaces
{
    public interface IUserAppService
    {
        bool Login(string userName, string password);
    }
}
