namespace Setups.Domain.Commands
{
    public sealed record CreateStateDomainCommand(string Name, string Code, IEnumerable<CreateCityDomainCommand> Cities);
}
