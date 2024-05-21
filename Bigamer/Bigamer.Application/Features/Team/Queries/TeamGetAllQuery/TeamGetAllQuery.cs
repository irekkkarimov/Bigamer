using Bigamer.Application.Requests.Team.Queries.TeamGetAllRequest;
using MediatR;

namespace Bigamer.Application.Features.Team.Queries.TeamGetAllQuery;

public class TeamGetAllQuery : IRequest<TeamGetAllResponse>
{
    public TeamGetAllQuery(TeamGetAllRequest request)
    {
        Props = request;
    }

    public TeamGetAllRequest Props { get; set; }
}