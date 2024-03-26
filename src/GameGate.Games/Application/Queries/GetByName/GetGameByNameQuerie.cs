using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Get;
public record GetGameByNameQuerie(string gameName) : IRequest<Game>;

