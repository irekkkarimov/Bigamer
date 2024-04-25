using Bigamer.Domain.Common;

namespace Bigamer.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;
    public List<UserInfo> UserInfos { get; set; } = new();
}