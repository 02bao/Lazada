using Lazada.Models;

namespace Lazada.Interface
{
    public interface ICartRepository
    {
        bool AddtoCart(long userId, CartItem_add cartItem_Add);
        List<Cart_see> GetCartByUserId(long userId);
        bool IncreaCartItem(long cartitemid);
        bool descreaCartItem(long cartitemid);
        bool RemoveCartItem( long cartitemId);
    }
}
