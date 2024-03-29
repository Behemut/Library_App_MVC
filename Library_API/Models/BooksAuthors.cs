﻿namespace Library_API.Models
{
    public class BooksAuthors
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Books Book { get; set; }
        public Authors Author { get; set; }
    }
}
