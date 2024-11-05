using GymInnowise.Shared.Sections.Interfaces;
using MediatR;

namespace GymInnowise.SectionService.Logic.Commands
{
    public sealed record RemoveFromSectionCommand<TRedundant>(Guid SectionId, Guid RedundantId) : IRequest
        where TRedundant : class, IRedundant;
}
