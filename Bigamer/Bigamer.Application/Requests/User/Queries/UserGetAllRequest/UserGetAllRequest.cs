namespace Bigamer.Application.Requests.User.Queries.UserGetAllRequest;

public class UserGetAllRequest
{
    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 10;
}