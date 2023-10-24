using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
	public class BookView
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string? Image { get; set; }
		public int Quantity { get; set; }
		public decimal OriginalPrice { get; set; }
		public decimal SellingPrice { get; set; }
		public string ISBN { get; set; }
		public int PageCount { get; set; }
		public bool IsSale { get; set; }
		public int PublicationYear { get; set; }
		public string PublisherName { get; set; }
		public string LanguageName { get; set; }
		public string AuthorName { get; set; }
		public string GenreName { get; set; }
	}
}
