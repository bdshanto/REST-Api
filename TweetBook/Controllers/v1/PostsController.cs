using TweetBook.Contracts.v1;
using TweetBook.Domain;

namespace TweetBook.Controllers.v1;

public class PostsController : Controller
{
    private ICollection<Post> _posts;
    public PostsController()
    {
        _posts = new List<Post>();
        for (var i = 1; i <= 5; i++)
        {
                _posts.Add(new Post(){Id = Guid.NewGuid(), Name = $"Name_{i}"});
        }
    }
    [HttpGet(ApiRoutes.Posts.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_posts);
    }
}