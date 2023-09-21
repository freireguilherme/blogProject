namespace Blog.Web.Models.Domain;

public class BlogPostComment
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Guid BlogPostId { get; set; }
    public Guid userId { get; set; }
    public DateTime DateAdded { get; set; }
}