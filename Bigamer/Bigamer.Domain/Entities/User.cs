using Bigamer.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Bigamer.Domain.Entities;

public class User : IdentityUser<Guid>, IEntity
{
    public Subscriber? Subscriber { get; set; }
    public UserInfo UserInfo { get; set; } = null!;
}