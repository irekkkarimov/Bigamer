namespace Bigamer.Application.Requests.User.Queries.UserGetAllRequest;

public class UserGetAllResponse
{
    public List<UserGetAllResponseItem> Users { get; set; } = new();
}