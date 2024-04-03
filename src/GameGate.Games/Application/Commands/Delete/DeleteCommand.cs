using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Delete;
public sealed record DeleteCommand (string GameId) : IRequest ;
