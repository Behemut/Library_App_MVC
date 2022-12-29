namespace Library_API.Models.DTO
{
    public class RolsDTO
    {
        public int RolId { get; set; }
        public string RolName { get; set; }
        public string RolDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }

        public ICollection<UsersDTO> Users { get; set; }
    }
}
