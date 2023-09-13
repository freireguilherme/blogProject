using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public class TagRepository : ITagInterface
    {
        public Task<Tag> AddAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> UpdateAsync(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
