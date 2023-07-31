using MediatR;
using Shrtly.Common;
using ShrtLy.Application.DTOs;

namespace ShrtLy.Application.Features;

public record GetAllShortLinksQuery(PaginationDto PaginationDto) : IRequest<Result<PaginatedList<LinkDto>>>;

