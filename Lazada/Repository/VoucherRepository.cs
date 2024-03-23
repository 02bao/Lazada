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
            };
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
    }
}
