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
        public bool AddnewVoucher(long shopid, Voucher_Add addvoucher, DateTime expiredate, long productvoucherid)
        {
            if(string.IsNullOrEmpty(addvoucher.title))
                return false;
            Shop shops = _context.Shops.SingleOrDefault(s => s.Id == shopid);
            if(shops == null) 
            {
                return false;
            }
            Product products = _context.Products.Where(s => s.ProductId == productvoucherid).FirstOrDefault();
            if(products == null)
            {
                //voucher cho tat ca san pham trong shop 
                Voucher allshop = new Voucher
                {
                    title = addvoucher.title,
                    Shop = shops,
                    Product = null,
                    //productvoucherid = null,
                    public_date = addvoucher.public_date.ToUniversalTime(),
                    expire_date = expiredate.ToUniversalTime(),
                    discount = addvoucher.discount,
                    User = null,
                };
                //Voucher cho tung san pham 
                _context.Vouchers.Add(allshop);
                shops.Voucher.Add(allshop);

            }
            else
            {
                Voucher someproduct = new Voucher
                {
                    title = addvoucher.title,
                    Shop = shops,
                    Product = products,
                    //productvoucherid = productvoucherid,
                    public_date=addvoucher.public_date.ToUniversalTime(),
                    expire_date=expiredate.ToUniversalTime(),
                    discount = addvoucher.discount,
                    User = null,
                };
                _context.Vouchers.Add(someproduct);
                shops.Voucher.Add(someproduct);
            }

            _context.SaveChanges();
            return true;
        }

        public List<Voucher_Product> GetVoucherbyCartId(long cartid)
        {
            List<Voucher_Product> response = new List<Voucher_Product>();
            Cart carts = _context.Carts.Include(s => s.Shops).ThenInclude(s => s.Voucher)
                                        .ThenInclude(s => s.Product).SingleOrDefault(s => s.Id == cartid);
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
                    if(voucher.expire_date.CompareTo(DateTime.Now) <= 0)
                    {
                        continue;
                    }
                    long? productvoucherid = null;
                    if (voucher.Product != null)
                    {
                        productvoucherid = voucher.Product.ProductId;
                    }
                    Voucher_Product tmp = new Voucher_Product
                    {
                        voucherId = voucher.Id,
                        title = voucher.title,
                        discount = voucher.discount,
                        productvoucherid = productvoucherid,
                        expire_date = voucher.expire_date,
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
                                       .ThenInclude(s => s.Product).FirstOrDefault();

            if (shops  == null || shops.Voucher == null)
            {
                return response;
            }
            else if(shops.Voucher.Any())
            {
                List<Voucher> shop_voucher = shops.Voucher;
                foreach(Voucher item in  shop_voucher)
                {
                    if(item.expire_date.CompareTo(DateTime.Now) <= 0)
                    {
                        continue;
                    }
                    long? productvoucherid = null; 
                    if(item.Product != null) 
                    {
                        productvoucherid = item.Product.ProductId;
                    }
                    Voucher_Product tmp = new Voucher_Product
                    {
                        voucherId = item.Id,
                        title = item.title,
                        discount = item.discount,
                        productvoucherid = productvoucherid,
                        expire_date = item.expire_date,
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
                shopvoucher = shopvoucher!.Where(s =>s.expire_date > DateTime.UtcNow).ToList();

                foreach(Voucher item in shopvoucher)
                {
                    long? productvoucherid = null;
                    if (item.Product != null)
                    {
                        productvoucherid = item.Product.ProductId;
                    }
                    Voucher_Product tmp = new Voucher_Product
                    {
                        voucherId=item.Id,
                        title = item.title,
                        discount = item.discount,
                        expire_date = item.expire_date,
                        productvoucherid = productvoucherid,
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
            var voucher = _context.Vouchers.Include(s => s.Shop).SingleOrDefault(s => s.Id == voucherid && s.expire_date > DateTime.UtcNow
            && s.User == null);
            if(voucher == null) { return false; }
            //kiem tra xem id cua nguoi tao ra shop co trung voi id cua nguoi muon lay voucher
            Shop shops = _context.Shops.Include(s => s.User).Where(s => s.User.Id == user.Id).FirstOrDefault();
            if(shops != null)
            {
                return false;
            }
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
            voucher.User = user;
            user.vouchers.Add(voucher);
            _context.SaveChanges();
            return true;
        }
    }
}
