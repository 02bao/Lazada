using Lazada.Models;

namespace Lazada.Interface
{
    public interface IVoucherRepository
    {
        bool AddnewVoucher(long shopid, Voucher_Add addvoucher);
    }
}
