namespace BackEnd.Application.DTOs
{
    public class UsersGetDto
    {
        public ulong Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;


    }
}
