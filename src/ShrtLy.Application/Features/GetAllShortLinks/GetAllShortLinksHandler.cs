using MediatR;
using Shrtly.Common;
using ShrtLy.Application.DTOs;
using ShrtLy.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShrtLy.Application.Features
{
    public class GetAllShortLinksHandler : IRequestHandler<GetAllShortLinksQuery, Result<PaginatedList<LinkDto>>>
    {
        private readonly ILinksRepository _repository;

        public GetAllShortLinksHandler(ILinksRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Result<PaginatedList<LinkDto>>> Handle(GetAllShortLinksQuery request, CancellationToken cancellationToken)
        {
            var links = await _repository.GetAllLinksPaginated(request.PaginationDto.Page, request.PaginationDto.PageSize);

            var linksDto = new PaginatedList<LinkDto>(links.TotalRecords, links.PageIndex, links.PageSize); ;

            foreach (var link in links)
            {
                var linkDto = new LinkDto
                {
                    Id = link.Id,
                    ShortUrl = link.ShortUrl,
                    Url = link.Url
                };

                linksDto.Add(linkDto);
            }
            return new Result<PaginatedList<LinkDto>>()
            {
                ResultStatus = ResultStatus.Success,
                Data = linksDto,
            };
        }
    }
}
