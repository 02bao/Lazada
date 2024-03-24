using Lazada.Models;

namespace Lazada.Interface
{
    public interface IAddressRepository
    {
        bool CreateNew(long userid, Address_User address_User);
        List<Address_Short> GetAddressByUserId(long userId);
        bool SetAddressDefault(long addressid);
    }
}
