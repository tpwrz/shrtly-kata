using MediatR;
using Shrtly.Common;

namespace ShrtLy.Application.Features;
public record RedirectCommand(string Url) : IRequest<Result<string>>;
