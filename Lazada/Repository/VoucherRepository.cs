using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;

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
            if(shopid == null) 
            {
                return false;
            }
                Voucher allshop = new Voucher
                {
                    title = addvoucher.title,
                    public_date = addvoucher.public_date,
                    expire_date = addvoucher.expire_date,
                    discount = addvoucher.discount,
                    list_product_applied = null
                };
                _context.Vouchers.Add(allshop);
                shops.Voucher.Add(allshop);
           
            _context.SaveChanges();
            return true;
        }
    }
}
