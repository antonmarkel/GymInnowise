using GymInnowise.Shared.Sections.Interfaces;
using MediatR;

namespace GymInnowise.SectionService.Logic.Commands
{
    public sealed record UpdateRedundantCommand<TRedundant>(TRedundant UpdateData) : IRequest
        where TRedundant : class, IRedundant;
}
