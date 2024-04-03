using Domain.Games;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetWithFilters;
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
