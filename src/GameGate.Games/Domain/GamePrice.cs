namespace Domain;

public sealed class GamePrice
{
    public decimal PriceMaxValue { get; set; }
    public decimal PriceMinValue { get; set; }
    public bool IsDirectly { get; set; }
}