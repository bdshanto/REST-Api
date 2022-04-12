using TweetBook.Controllers.v1.Requests;
using TweetBook.Services;

namespace TweetBook.Controllers.v1;

public class PostsController : Controller
{

    private readonly IPostService _iPostService;

    public PostsController(IPostService iPostService)
    {
        _iPostService = iPostService;
    }

    [HttpGet(ApiRoutes.Posts.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_iPostService.GetAll());
    }
    [HttpGet(ApiRoutes.Posts.Get)]
    public IActionResult Get(Guid postId)
    {
        var post = _iPostService.GetById(postId);
        if (post == null) return NotFound();
        return Ok(post);
    }

    [HttpPost(ApiRoutes.Posts.Create)]
    public IActionResult Create([FromBody] CreatePostRequest postResponse)
    {
        var post = new Post()
        {
            Id = postResponse.Id,
            Name = postResponse.Name
        };

        if (post.Id != Guid.Empty)
        {
            post.Id = Guid.NewGuid();
        }

        _iPostService.GetAll().Add(post);
        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString())}";

        var response = new PostResponse { Id = post.Id.ToString() };
        return Created(locationUrl, response);

    }
}