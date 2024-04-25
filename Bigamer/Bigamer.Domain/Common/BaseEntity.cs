using Bigamer.Domain.Common.Interfaces;

namespace Bigamer.Domain.Common;

/// <summary>
/// Базовая сущность
/// </summary>
public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }    
}