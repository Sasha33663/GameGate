using Application.Common.Inteefaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Delete;
public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
{
    private readonly IGameRepository _gameRepository;
    public DeleteCommandHandler (IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        _gameRepository.GetGameAsync(request);
        _gameRepository.DeleteAsync(request);
    }
}
