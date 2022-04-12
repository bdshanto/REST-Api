namespace TweetBook.Services;

public interface IPostService
{
    ICollection<Post> GetAll();
    Post GetById(Guid id);

}