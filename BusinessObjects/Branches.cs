using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Brands
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
