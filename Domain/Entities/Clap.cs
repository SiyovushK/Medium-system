namespace Domain.Entities;

public class Clap
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public int Count { get; set; }
    public DateTime CreatedAt { get; set; }
}