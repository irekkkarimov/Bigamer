using Bigamer.Application.Requests.User.Queries.UserGetAllRequest;
using MediatR;

namespace Bigamer.Application.Features.User.Queries.UserGetAllQuery;

public class UserGetAllQuery : IRequest<UserGetAllResponse>
{
    public UserGetAllQuery(UserGetAllRequest request)
    {
        Props = request;
    }

    public UserGetAllRequest Props { get; set; }   
}