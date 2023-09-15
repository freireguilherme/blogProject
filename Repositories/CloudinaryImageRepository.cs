namespace Blog.Web.Repositories;

public class CloudinaryImageRepository : IImageRepository
{
    public Task<string> UploadAsync(IFormFile formFile)
    {
        throw new NotImplementedException();
    }
}