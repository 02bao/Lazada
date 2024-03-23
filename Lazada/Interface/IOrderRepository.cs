﻿using Lazada.Models;

namespace Lazada.Interface
{
    public interface IOrderRepository
    {
        bool AddtoOrder(long userId, long cartitemId);
        //bool AddtoOrder(List<long> list_cart_item_id);
        List<Order_User> GetOrderByUserId(long userId);
    }
}