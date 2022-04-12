using TweetBook.Contracts.v1;
using TweetBook.Controllers.v1.Response;
using TweetBook.Domain;

namespace TweetBook.Controllers.v1;

public class PostsController : Controller
{
    private readonly ICollection<Post> _posts;

    public PostsController()
    {
        _posts = new List<Post>();
        for (var i = 1; i <= 5; i++) _posts.Add(new Post() { Id = $"{Guid.NewGuid()}", Name = $"Name_{i}" });
    }

    [HttpGet(ApiRoutes.Posts.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_posts);
    }

    [HttpPost(ApiRoutes.Posts.Create)]
    public IActionResult Create([FromBody] Post post)
    {
        if (string.IsNullOrEmpty(post.Id))
        {
            post.Id = $"{Guid.NewGuid()}";
        }

        _posts.Add(post);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString())}";

        var response = new PostResponse { Id = post.Id };
        return Created(locationUrl, response);

    }
}