﻿using Domain;
using MediatR;

namespace Application.Queries.GetAll;

public class GetAllGamesQuery : IRequest<List<Game>>
{
}