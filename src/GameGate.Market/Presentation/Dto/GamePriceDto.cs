﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Dto;
public sealed class GamePriceDto
{
    public decimal PriceMaxValue { get; set; }
    public decimal PriceMinValue { get; set; }
    public bool IsDirectly { get; set; }
}