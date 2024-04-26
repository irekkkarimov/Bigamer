using Bigamer.Domain.Common;
using Bigamer.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Bigamer.Domain.Entities;

public class Role : IdentityRole<Guid>, IEntity
{
}