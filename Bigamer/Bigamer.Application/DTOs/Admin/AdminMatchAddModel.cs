using Bigamer.Application.Features.Game.Queries.GameGetAllQuery;
using Bigamer.Application.Requests.Game.Queries.GameGetAllRequest;

namespace Bigamer.Application.DTOs.Admin;

public class AdminMatchAddModel
{
    public GameGetAllResponse Games { get; set; } = new();
}