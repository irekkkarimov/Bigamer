namespace Bigamer.Application.Requests.Game.Queries.GameGetAllRequest;

public class GameGetAllResponseItem
{
    public Guid GameId { get; set; }
    public string GameName { get; set; } = null!;
}