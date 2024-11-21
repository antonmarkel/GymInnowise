using GymInnowise.Shared.Sections.Interfaces;
using MediatR;

namespace GymInnowise.SectionService.Logic.Commands
{
    public sealed record CreateRedundantCommand<TRedundant>(TRedundant Data) : IRequest
        where TRedundant : class, IRedundant;
}
