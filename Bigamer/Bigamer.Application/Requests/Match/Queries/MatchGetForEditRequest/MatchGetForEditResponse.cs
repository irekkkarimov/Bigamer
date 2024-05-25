using Bigamer.Application.DTOs.Links;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using Bigamer.Shared.Enums;

namespace Bigamer.Application.Requests.Match.Queries.MatchGetForEditRequest;

public class MatchGetForEditResponse
{
    public Guid MatchId { get; set; }
    public string GameName { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public double? Prize { get; set; }
    public List<MatchGetAllResponseTeam> Teams { get; set; } = new();
    public List<GetLink> Links { get; set; } = new();
}