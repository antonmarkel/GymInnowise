using GymInnowise.Shared.Sections.Interfaces;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Queries
{
    public sealed record GetSectionRelationQuery<TSectionRelation>(Guid SectionId, Guid RelatedId)
        : IRequest<OneOf<TSectionRelation, NotFound>>
        where TSectionRelation : class, ISectionRelation;
}
