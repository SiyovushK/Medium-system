namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
}