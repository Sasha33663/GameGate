﻿using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAll;
public class GetAllGamesQuerie : IRequest <List<Game>>
{
}
