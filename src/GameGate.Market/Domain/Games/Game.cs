namespace Domain.Games;

public class Game //TODO: сделать из дтошки полноценный класс с конструктором и валидацией
{
    public string GameName { get; set; }
    public string Description { get; set; }
    public string GamePreviewUrl { get; set; }
    public string Author { get; set; }
    public Filters Filters { get; set; }
    public GamePrice Price { get; set; }
}