namespace Setups.Domain.Commands
{
    using System;

    public sealed record CreateServiceProvisionDomainCommand(DateTime StartOn, DateTime? EndOn);
}
