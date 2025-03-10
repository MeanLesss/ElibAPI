namespace ElibAPI.Controllers_BookAPI
{
    public class AddOrUpdateBookDto
    {
        public string user_token { get; set; }
        public int? book_id { get; set; }
        public string? title { get; set; }
        public int? group_id { get; set; }
    }
}
