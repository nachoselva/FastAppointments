namespace Setups.Domain.Commands
{
    public sealed record CreateLocationDomainCommand(string Name, string? Floor, string? Room);
}
