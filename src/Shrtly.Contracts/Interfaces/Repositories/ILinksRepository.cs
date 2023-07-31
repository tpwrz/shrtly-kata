using Shrtly.Common;
using ShrtLy.Domain;

namespace ShrtLy.Contracts
{
    public interface ILinksRepository
    {
        Task<Guid> CreateLink(LinkEntity entity);
        Task<bool> CheckUrlExists(string url);
        Task<string> FindUrl (string url);
        Task<PaginatedList<LinkEntity>> GetAllLinksPaginated(int page, int pageSize); 
    }
}