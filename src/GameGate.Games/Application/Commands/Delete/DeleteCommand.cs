using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Delete;
public record DeleteCommand (string GameName, string GameId, string PreviewId) : IRequest;
