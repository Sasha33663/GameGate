﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Create;
public  record CreateCommand (string Name,string Discription,string?  GamePreviewName, Stream? GamePreview, string Genre, string Kind,string Creator, string coockie) :IRequest;

