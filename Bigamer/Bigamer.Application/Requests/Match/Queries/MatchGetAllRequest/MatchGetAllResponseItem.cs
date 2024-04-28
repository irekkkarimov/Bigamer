using Bigamer.Application.DTOs.Links;

namespace Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;

public class MatchGetAllResponseItem
{
    public string GameName { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public double Prize { get; set; }
    public List<MatchGetAllResponseTeam> Teams { get; set; } = new();
    public List<GetLink> Links { get; set; } = new();
}