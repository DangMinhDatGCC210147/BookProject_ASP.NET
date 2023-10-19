﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class BookAuthor
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookImage { get; set; }
        public double ReviewRate { get; set; }
        public int TotalSold { get; set; }
    }
}
