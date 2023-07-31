using MediatR;
using Shrtly.Common;
using ShrtLy.Application.Services;
using ShrtLy.Contracts;
using ShrtLy.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShrtLy.Application.Features
{


    public class GetToShortLinkHandler : IRequestHandler<GetToShortLinkCommand, Result<string>>
    {
        private readonly IShorteningService _service;
        private readonly ILinksRepository _repository;

        private const string _PREFIX = "https://localhost:5001/";

        public GetToShortLinkHandler(IShorteningService service, ILinksRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public async Task<Result<string>> Handle(GetToShortLinkCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.CheckUrlExists(request.Url);
            if (exists)
            {
                return new Result<string>()
                {
                    ResultStatus = ResultStatus.Conflict,
                    Message = "Shortened URL already exists",
                };
            }
            if (CheckIsAlreadyShort(request.Url))
            {
                return new Result<string>()
                {
                    ResultStatus = ResultStatus.BadRequest,
                    Message = "This is an already shortened URL",
                };

            }
            var link = await _service.ProcessLink(request.Url);

            var linkEntity = new LinkEntity
            {
                Id = Guid.NewGuid(),
                Url = request.Url,
                ShortUrl = link
            };

            var result = await _repository.CreateLink(linkEntity);

            return new Result<string>()
            {
                ResultStatus = ResultStatus.Success,
                Data = _PREFIX+ linkEntity.ShortUrl,
            };
        }

        public bool CheckIsAlreadyShort(string url)
        {
            var trimmed  = url.Replace(_PREFIX, "");
            if (!trimmed.Contains("/"))
            {
                return true;
            }
            return false;
        }
    }
}