namespace Domain.DTOs;

public class UserNameAndAllArticle
{
    public string? Username { get; set; }
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Status { get; set; }
}