using Microsoft.EntityFrameworkCore;
using Shrtly.Common;
using ShrtLy.Contracts;
using ShrtLy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShrtLy.Infrastructure
{
    public class LinksRepository : ILinksRepository
    {
        private readonly ShrtLyContext _context;

        public LinksRepository(ShrtLyContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateLink(LinkEntity entity)
        {
            var result  = await _context.Links.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
        public async Task<bool> CheckUrlExists(string url)
        {
            if (string.IsNullOrEmpty(url))
                return true;
            return await _context.Links
                .AnyAsync(l => l.Url == url);
        }

        public async Task<PaginatedList<LinkEntity>> GetAllLinksPaginated(int page, int pageSize)
        {
            var query = _context.Links;

            var totalRecords = await query.CountAsync();

            var result = await query.Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();

            return new PaginatedList<LinkEntity>(result, totalRecords, page, pageSize);
        }

        public async Task<string> FindUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;
            return await _context.Links.Where(l=> l.ShortUrl == url).Select(l=>l.Url).SingleAsync();
        }
    }
}
