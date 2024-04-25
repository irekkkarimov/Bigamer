namespace Bigamer.Domain.Common.Interfaces;

public interface IEntity
{
    /// <summary>
    /// Id сущности
    /// </summary>
    Guid Id { get; set; }
}