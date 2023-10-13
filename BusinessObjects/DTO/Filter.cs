namespace BusinessObjects.DTO
{
    public class Filter
    {
        public List<GetNameAndQuantity> genres { get; set; }
        public List<GetNameAndQuantity> publishers { get; set; }
        public List<GetNameAndQuantity> languages { get; set; }
        public List<GetNameAndQuantity> authors { get; set; }

    }
}
