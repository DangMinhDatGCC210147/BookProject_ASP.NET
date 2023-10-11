using BusinessObjects;
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        Discount SaveDiscount(Discount discount);
        List<Discount> GetDiscounts();
        Discount GetDiscountById(int id);
        void DeleteDiscountById(Discount discount);
        Discount UpdateDiscount(Discount discount);
    }
}
