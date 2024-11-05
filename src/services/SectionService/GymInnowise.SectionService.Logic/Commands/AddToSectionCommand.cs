using GymInnowise.Shared.Sections.Interfaces;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Commands
{
    public sealed record AddToSectionCommand<TRedundant>(Guid SectionId, Guid RedundantId)
        : IRequest<OneOf<Success, NotFound>>
        where TRedundant : class, IRedundant;
}
