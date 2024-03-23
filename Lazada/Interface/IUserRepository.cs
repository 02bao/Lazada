using Lazada.Models;

namespace Lazada.Interface
{
    public interface IUserRepository
    {
        ICollection<User> GetUser();
        User GetById(long id);
        bool Register(User_register userRegister);
        long Login(User_login user);
        bool Update(User_update user);
        bool Delete(long id);
        Task<string> ForgetPassword(User_Forget password);
    }
}
