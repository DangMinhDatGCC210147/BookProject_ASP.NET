using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Authors
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
