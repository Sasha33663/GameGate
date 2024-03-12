﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;
public class Game
{
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] GamePreview { get; set; }
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public string Creator { get; set; }
    public string Genre { get; set; }
    public string Kind { get; set; }

}
