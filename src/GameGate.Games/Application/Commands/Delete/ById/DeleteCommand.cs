using MediatR;

namespace Application.Commands.Delete.ById;
public sealed record DeleteCommand(string GameId) : IRequest;