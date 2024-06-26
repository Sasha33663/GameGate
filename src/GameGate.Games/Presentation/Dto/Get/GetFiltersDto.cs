﻿namespace Presentation.Dto.Get;

public class GetFiltersDto
{
    public string? GameName { get; set; }
    public string? Creator { get; set; }
    public string? Genre { get; set; }
    public string? Kind { get; set; }
    public decimal? PriceMaxValue { get; set; }
    public decimal? PriceMinValue { get; set; }
    public bool? IsDirectly { get; set; }
}