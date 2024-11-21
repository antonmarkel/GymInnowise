using GymInnowise.Shared.Sections.Interfaces;
using MediatR;

namespace GymInnowise.SectionService.Logic.Commands
{
    public sealed record RemoveFromSectionCommand<TSectionRelation>(Guid SectionId, Guid RelatedId) : IRequest
        where TSectionRelation : class, ISectionRelation;
}
