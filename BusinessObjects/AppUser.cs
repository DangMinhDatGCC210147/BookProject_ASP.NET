using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessObjects
{
	public class AppUser :  IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string? GoogleId { get; set; }
		public string? FacebookId { get; set; }
		public ICollection<Cart>? Carts { get; set; }
		public ICollection<Favourite>? Favourites { get; set; }
		public ICollection<Review>? Reviews { get; set; }
		public ICollection<Order>? Orders { get; set; }
	}
}
