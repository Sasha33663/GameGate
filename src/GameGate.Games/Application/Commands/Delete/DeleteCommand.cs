using MediatR;

namespace Application.Commands.Delete;
public sealed record DeleteCommand(string GameId) : IRequest;