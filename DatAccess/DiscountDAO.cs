using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DiscountDAO
    {
        public static List<Discount> GetDiscounts()
        {
            var listDiscounts = new List<Discount>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listDiscounts = context.Discounts.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listDiscounts;
        }

        public static Discount FindDiscountById(int id)
        {
            var discount = new Discount();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    discount = context.Discounts.Find(id);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return discount;
        }
        public static void SaveDiscount(Discount discount)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Discounts.Add(discount);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateDiscount(Discount discount)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Discount>(discount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteDiscount(Discount discount)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Discounts.Remove(FindDiscountById(discount.Id));
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
