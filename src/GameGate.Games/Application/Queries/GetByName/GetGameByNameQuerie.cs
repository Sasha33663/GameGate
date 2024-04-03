using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetByName;
public record GetGameByNameQuery(string gameName) : IRequest<Game>;

