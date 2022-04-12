namespace TweetBook.Services;

public class PostService : IPostService
{
    private readonly ICollection<Post> _posts;
    public PostService()
    {
        _posts = new List<Post>();
        for (var i = 1; i <= 5; i++) _posts.Add(new Post() { Id = Guid.NewGuid(), Name = $"Post_Name_{i}" });
    }

    public ICollection<Post> GetAll()
    {
        return _posts;
    }
    public Post GetById(Guid postId)
    {

        var post = _posts.SingleOrDefault(c => c.Id == postId);
        if (post == null)
        {

        }
        return post;
    }


}