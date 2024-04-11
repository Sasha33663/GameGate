using Domain.Games;
using MediatR;

namespace Application.Queries.Games.GetWithFilters;
public sealed record GetGameWithFiltersQuery
(
     string? GameName,
     string? Creator,
     string? Genre,
     string? Kind,
     decimal? PriceMaxValue,
     decimal? PriceMinValue,
     bool? IsDirectly
) : IRequest<List<Game>>;