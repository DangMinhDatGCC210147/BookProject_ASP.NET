using BusinessObjects;
using DataAccess;
using DatAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        public void DeleteDiscountById(Discount discount) => DiscountDAO.DeleteDiscount(discount);

        public Discount GetDiscountById(int id) => DiscountDAO.FindDiscountById(id);
        public Discount GetDiscountByName(string name) => DiscountDAO.FindDiscountByName(name);

        public List<Discount> GetDiscounts() => DiscountDAO.GetDiscounts();

        public Discount SaveDiscount(Discount discount) => DiscountDAO.SaveDiscount(discount);

        public Discount UpdateDiscount(Discount discount) => DiscountDAO.UpdateDiscount(discount);
    }
}
