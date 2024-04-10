using Domain;
using MediatR;

namespace Application.Queries.GetWithFIlters;
public sealed record GetGameWithFiltersQuery
(string? GameName,
     string? Creator,
     string? Genre,
     string? Kind,
     decimal? PriceMaxValue,
     decimal? PriceMinValue,
     bool? IsDirectly
) : IRequest<List<Game>>;