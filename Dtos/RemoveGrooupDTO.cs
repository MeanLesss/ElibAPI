namespace ElibAPI.DTOs;
public struct RemoveGroupDTO {
    public int group_id { get; set; } 
    public string? api_token { get; set; }   
    public string user_token { get; set; }
}