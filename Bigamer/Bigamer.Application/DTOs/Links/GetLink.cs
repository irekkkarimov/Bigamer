using Bigamer.Shared.Enums;

namespace Bigamer.Application.DTOs.Links;

public class GetLink
{
    public LinkType ServiceName { get; set; }
    public string Link { get; set; } = null!;
}