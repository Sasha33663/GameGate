namespace Presentation.Dto;

public sealed class GamePriceDto  //TODO: удалить
{
    public decimal PriceMaxValue { get; set; }
    public decimal PriceMinValue { get; set; }
    public bool IsDirectly { get; set; }
}