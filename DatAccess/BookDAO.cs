﻿using BusinessObjects;
using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class BookDAO
	{
        public static List<Book> GetProducts()
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var books = context.Books
                        .Include(book => book.Publisher)
                        .Include(book => book.Author)
                        .Include(book => book.Genre)
                        .Include(book => book.Language)
                        .ToList();

                    return books;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Book> FindProductByName(string titleToSearch)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					return context.Books.Where(book => book.Title.Contains(titleToSearch)).ToList();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static Book FindProductById(int id)
		{
			var product = new Book();
			try
			{
				using (var context = new ApplicationDBContext())
				{
					product = context.Books.Find(id);
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return product;
		}
		public static Book SaveProduct(Book book)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Books.Add(book);
					context.SaveChanges();
					return book;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public static Book UpdateProduct(Book book)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Entry<Book>(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					context.SaveChanges();
					return book;
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteProduct(Book book)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Books.Remove(FindProductById(book.Id));
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		//Payment 
		public static bool UpdateQuantity(int BookId, int soldQuantity)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					Book getBook = FindProductById(BookId);
					if (getBook != null)
					{
						getBook.Quantity = getBook.Quantity - soldQuantity;
						
                        if (UpdateProduct(getBook) != null)
                        {
							return true;
                        }
                    }
					return false;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
