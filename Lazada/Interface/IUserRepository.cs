using Lazada.Models;

namespace Lazada.Interface
{
    public interface IUserRepository
    {
        ICollection<User> GetUer();
        bool Register(User user);
        bool Login(User_login user);  
    }
}
