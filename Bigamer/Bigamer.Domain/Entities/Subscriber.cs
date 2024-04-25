using Bigamer.Domain.Common;

namespace Bigamer.Domain.Entities;

public class Subscriber : BaseEntity
{
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}