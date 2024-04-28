using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Queries.MatchGetAllQuery;

public class MatchGetAllQuery : IRequest<MatchGetAllResponse>
{
    public MatchGetAllQuery(MatchGetAllRequest request)
    {
        Props = request;
    }

    public MatchGetAllRequest Props { get; set; }
}