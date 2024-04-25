using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Interfaces;

public interface IDbContext : IDisposable
{
    DbContext Context { get; }
}