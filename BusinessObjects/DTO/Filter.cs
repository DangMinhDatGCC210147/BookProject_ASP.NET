namespace BusinessObjects.DTO
{
    public class Filter
    {
        public List<GetNameAndQuantity> Genres { get; set; }
        public List<GetNameAndQuantity> Publishers { get; set; }
        public List<GetNameAndQuantity> Languages { get; set; }
        public List<GetNameAndQuantity> Authors { get; set; }

    }
}
