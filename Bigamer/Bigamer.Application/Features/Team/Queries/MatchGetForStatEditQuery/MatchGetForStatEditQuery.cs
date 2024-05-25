using Bigamer.Application.Requests.Team.Queries.MatchGetForStatEditRequest;
using MediatR;

namespace Bigamer.Application.Features.Team.Queries.MatchGetForStatEditQuery;

public class MatchGetForStatEditQuery : IRequest<MatchGetForStatEditResponse>
{
    public MatchGetForStatEditQuery(MatchGetForStatEditRequest props)
    {
        Props = props;
    }

    public MatchGetForStatEditRequest Props { get; set; }
}