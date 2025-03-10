using Swashbuckle.AspNetCore.Annotations;

namespace ElibAPI.Dtos
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
        [SwaggerSchema(Description = "The role of the user. Possible values: Student, Teacher, Admin, Director.")]
        public string Role { get; set; } = null!;
        public int? GroupId { get; set; } 
    }
}
