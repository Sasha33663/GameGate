using Application.Common.Intefaces;
using MediatR;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Delete.ByName;
public class DeleteByNameCommandHandler : IRequestHandler<DeleteByNameCommand>
{
    private readonly IGameRepository _gameRepository;

    public DeleteByNameCommandHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task Handle(DeleteByNameCommand request, CancellationToken cancellationToken)
    {
        _gameRepository.DeleteGameByName(request.gameName);
    }
}
