using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        void SaveDiscount(Discount discount);
        Discount GetDiscountById(int id);
        void DeleteDiscountById(Discount discount);
        void UpdateDiscount(Discount discount);
        List<Discount> GetDiscounts();
    }
}
