using Bigamer.Domain.Common;

namespace Bigamer.Domain.Entities;

public class UserInfo : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? NickName { get; set; }
    public bool IsBanned { get; set; }
    public string? ImageUrl { get; set; }
}