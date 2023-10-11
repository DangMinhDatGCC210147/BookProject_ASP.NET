﻿using BusinessObjects;

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

        public static Discount SaveDiscount(Discount discount)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Discounts.Add(discount);
                    context.SaveChanges();
                    return discount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return null;
            }
        }

        public static Discount UpdateDiscount(Discount discount)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Discount>(discount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return discount;
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
