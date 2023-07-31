using MediatR;
using Shrtly.Common;

namespace ShrtLy.Application.Features;
public record GetToShortLinkCommand(string Url) : IRequest<Result<string>>;
