namespace Setups.Domain.Commands
{
    public sealed record CreateCountryDomainCommand(string Name, string Code, IEnumerable<CreateStateDomainCommand> States);
}
