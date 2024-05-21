namespace Bigamer.Application.Requests.User.Queries.UserGetAllRequest;

public class UserGetAllResponse
{
    public List<UserGetAllResponseItem> Users { get; set; } = new();
    public int CurrentOffset { get; set; }
    public int CurrentLimit { get; set; }
    public int TotalCount { get; set; }
}