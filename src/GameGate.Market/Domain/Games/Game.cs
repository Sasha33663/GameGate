namespace Domain.Games;

public class Game 
{
    public string GameName { get; set; }
    public string Description { get; set; }
    public string GamePreviewUrl { get; set; }
    public string GameId { get; set; }
    public string AuthorId { get; set; }
    public string AuthorName { get; set; }
    public Filters Filters { get; set; }
    public GamePrice Price { get; set; }

}