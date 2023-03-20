namespace Domain
{
    public class Authors
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }
        public ICollection<BooksAuthors> Books { get; set; } = new List<BooksAuthors>();
    }
}
