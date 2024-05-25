using Bigamer.Application.Requests.User.Queries.UserGetAllRequest;
using Bigamer.Application.Requests.User.Queries.UserGetForEditRequest;
using MediatR;

namespace Bigamer.Application.Features.User.Queries.UserGetForEditQuery;

public class UserGetForEditQuery : IRequest<UserGetAllResponseItem>
{
    public UserGetForEditQuery(UserGetForEditRequest request)
    {
        Props = request;
    }


    public UserGetForEditRequest Props { get; set; }
}