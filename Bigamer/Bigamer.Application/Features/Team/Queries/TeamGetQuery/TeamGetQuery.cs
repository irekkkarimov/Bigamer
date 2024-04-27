using Bigamer.Application.DTOs.Team.Queries.TeamGetRequest;
using MediatR;

namespace Bigamer.Application.Features.Team.Queries.TeamGetQuery;

public class TeamGetQuery : IRequest<TeamGetResponse>
{
    public TeamGetQuery(TeamGetRequest request)
    {
        Props = request;
    }

    public TeamGetRequest Props { get; set; }
}