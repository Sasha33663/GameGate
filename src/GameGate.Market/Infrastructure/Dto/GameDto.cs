namespace Infrastructure.Dto;

public class GameDto  //TODO: удалить
{
    public string GameName { get; set; }
    public string Description { get; set; }
    public string GamePreviewUrl { get; set; }
    public Guid GameId { get; set; }
    public string Creator { get; set; }
    public string Genre { get; set; }
    public string Kind { get; set; }
}