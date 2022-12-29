namespace Library_API.Models.DTO
{
    public class UsersDTO
    {
        public int UserId { get; set; }

        public int RefUserPersonId { get; set; }

        public UsersPerson UserPerson { get; set; }

        public string Password { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatedBy { get; set; }


        public DateTime? LastLoginDate { get; set; }


        public int RefRolsId { get; set; }


    }
}
