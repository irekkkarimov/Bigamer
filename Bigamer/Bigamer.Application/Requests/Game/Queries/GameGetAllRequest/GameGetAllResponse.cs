namespace Bigamer.Application.Requests.Game.Queries.GameGetAllRequest;

public class GameGetAllResponse
{
    public List<GameGetAllResponseItem> Games { get; set; } = new();
}