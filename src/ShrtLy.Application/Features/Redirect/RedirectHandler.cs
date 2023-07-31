using MediatR;
using Shrtly.Common;
using ShrtLy.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShrtLy.Application.Features.Redirect
{
    public class GetToShortLinkHandler : IRequestHandler<RedirectCommand, Result<string>>
    {
        private readonly ILinksRepository _repository;
        public GetToShortLinkHandler(ILinksRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Result<string>> Handle(RedirectCommand request, CancellationToken cancellationToken)
        {
            var url = await _repository.FindUrl(request.Url);
            if (url is null)
            {
                return new Result<string>()
                {
                    ResultStatus = ResultStatus.NotFound,
                    Message = " URL not found",
                };
            }
            return new Result<string>()
            {
                ResultStatus = ResultStatus.Success,
                Data = url,
            };
        }

    }
}
