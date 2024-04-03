﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Create;
public sealed record CreateCommand (string Name,
    string Discription,
    string?  GamePreviewName,
    Stream? GamePreview,
    string Genre,
    string Kind,
    string Creator,
    decimal PriceMaxValue,
    decimal PriceMinValue,
    bool IsDirectly,
    string coockie) :IRequest;

