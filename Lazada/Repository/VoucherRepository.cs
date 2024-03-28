using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.EntityFrameworkCore;

namespace Lazada.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly LazadaDBContext _context;

        public VoucherRepository(LazadaDBContext context)
        {
            _context = context;
        }
        public bool AddnewVoucher(long shopid, Voucher_Add addvoucher)
        {
            if(string.IsNullOrEmpty(addvoucher.title))
                return false;
            Shop shops = _context.Shops.SingleOrDefault(s => s.Id == shopid);
            if(shops == null) 
            {
                return false;
            }
            Voucher allshop = new Voucher
            {
                title = addvoucher.title,
                public_date = addvoucher.public_date,
                expire_date = addvoucher.expire_date,
                discount = addvoucher.discount,
                User = null,
            };
            //Voucher cho tung san pham 
            _context.Vouchers.Add(allshop);
            shops.Voucher.Add(allshop);
           
            _context.SaveChanges();
            return true;
        }

        public List<Voucher_Product> GetVoucherbyCartId(long cartid)
        {
            List<Voucher_Product> response = new List<Voucher_Product>();
            Cart carts = _context.Carts.Include(s => s.Shops).ThenInclude(s => s.Voucher)
                                        .SingleOrDefault(s => s.Id == cartid);
            if (carts == null)
            {
                return response;
            }
            // van de quan ly thoi gian 
            else if(carts.Shops.Voucher.Any())
            {
                List<Voucher> vouchers = carts.Shops.Voucher;
                foreach(Voucher voucher in vouchers)
                {
                    Voucher_Product tmp = new Voucher_Product
                    {
                        voucherId = voucher.Id,
                        title = voucher.title,
                        discount = voucher.discount,
                    };
                    response.Add(tmp);
                }
            }
            return response;
        }

        public List<Voucher_Product> GetVoucherbyShopid(long shopid)
        {
            List<Voucher_Product> response = new List<Voucher_Product>();
            Shop shops = _context.Shops.Where(s => s.Id == shopid).Include(s => s.Voucher)
                                        .FirstOrDefault();
            if(shops  == null)
            {
                return response;
            }
            else if(shops.Voucher.Any())
            {
                List<Voucher> shop_voucher = shops.Voucher;
                foreach(Voucher item in  shop_voucher)
                {
                    Voucher_Product tmp = new Voucher_Product
                    {
                        voucherId = item.Id,
                        title = item.title,
                        discount = item.discount,
                    };
                    response.Add(tmp);
                }
            }
            return response;

        }

        public List<Voucher_Product> WareHouseShopVoucher(long userid)
        {
            List<Voucher_Product> response = new List<Voucher_Product>();
            User user = _context.Users.Where(s => s.Id == userid)
                                       .Include(s => s.vouchers).FirstOrDefault();
            if(user == null) 
            {
                return response;
            }
            List<Voucher>? shopvoucher = user.vouchers.ToList();
            if(shopvoucher != null && shopvoucher.Any())
            {
                shopvoucher = shopvoucher!.Where(s => s.list_user_applied == null || 
                    !s.list_user_applied.Contains(userid.ToString())).ToList();

                foreach(Voucher item in shopvoucher)
                {
                    Voucher_Product tmp = new Voucher_Product
                    {
                        voucherId=item.Id,
                        title = item.title,
                        discount = item.discount,
                    };
                    response.Add(tmp);
                }
            }
            return response;

        }

        public bool warehouse_save(long userid, long voucherid)
        {
            var user = _context.Users.Include(u => u.vouchers).SingleOrDefault(s => s.Id==userid);
            if(user == null) { return false;}
            var voucher = _context.Vouchers.SingleOrDefault(s => s.Id == voucherid);
            if(voucher == null) { return false; }
           if(user.vouchers == null)
            {
                user.vouchers = new List<Voucher>();
            }
           else
            {
                if(user.vouchers.Contains(voucher))
                {
                    return false;
                }

            }
            user.vouchers.Add(voucher);
            _context.SaveChanges();
            return true;
        }
    }
}
