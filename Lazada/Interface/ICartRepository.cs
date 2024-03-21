using Lazada.Models;

namespace Lazada.Interface
{
    public interface ICartRepository
    {
        bool AddtoCart(long userId, long ProductId, CartItem_add cartItem_Add);
        List<Cart_see> GetCartByUserId(long userId);
    }
}
