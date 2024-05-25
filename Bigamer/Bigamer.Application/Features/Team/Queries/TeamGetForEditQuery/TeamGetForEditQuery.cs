using Bigamer.Application.Requests.Team.Queries.TeamGetForEditRequest;
using MediatR;

namespace Bigamer.Application.Features.Team.Queries.TeamGetForEditQuery;

public class TeamGetForEditQuery : IRequest<TeamGetForEditResponse>
{
    public TeamGetForEditQuery(TeamGetForEditRequest request)
    {
        Props = request;
    }

    public TeamGetForEditRequest Props { get; set; }
}