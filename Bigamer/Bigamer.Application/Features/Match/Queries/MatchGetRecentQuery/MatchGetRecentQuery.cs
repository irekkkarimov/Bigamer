using Bigamer.Application.Requests.Match.Queries.MatchGetRecentRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Queries.MatchGetRecentQuery;

public class MatchGetRecentQuery : IRequest<MatchGetRecentResponse>
{
    public MatchGetRecentQuery(MatchGetRecentRequest request)
    {
        Props = request;
    }

    public MatchGetRecentRequest Props { get; set; }
}