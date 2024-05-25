using Bigamer.Application.Requests.Match.Queries.MatchGetForEditRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Queries.MatchGetForEditQuery;

public class MatchGetForEditQuery : IRequest<MatchGetForEditResponse>
{
    public MatchGetForEditQuery(MatchGetForEditRequest request)
    {
        Props = request;
    }

    public MatchGetForEditRequest Props { get; set; }
}