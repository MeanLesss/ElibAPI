using ELibAPI.Dtos;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ElibAPI.DTOs;
public class GroupDTO:UserTokenDTO {
    // public string user_token { get; set; }
    [Required]
    [MinLength(5)]
    public string Name { get; set; }
    [SwaggerSchema(Description = "The Group for values: Student, Teacher, Admin, Director.")]
    public string? Group_For { get; set; } 

    // public string? api_token { get; set; }

}