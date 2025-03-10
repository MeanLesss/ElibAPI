using System.ComponentModel.DataAnnotations;

namespace ELibAPI.Dtos
{
    public class BookDto:UserTokenDTO
    {
        // public string user_token { get; set; } 
        public int? Id { get; set; }
        [Required]
        [MinLength(5)]
        public string? Title { get; set; }
        public int? UserId { get; set; }    
        public int? group_id { get; set; }
        public string? search { get; set; }
        public string? sort_order { get; set; }
    }
}