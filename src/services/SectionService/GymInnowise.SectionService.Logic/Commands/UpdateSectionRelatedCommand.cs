using GymInnowise.Shared.Sections.Interfaces;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Commands
{
    public sealed record UpdateSectionRelatedCommand<TSectionRelation>(TSectionRelation UpdatedData)
        : IRequest<OneOf<Success, NotFound>>
        where TSectionRelation : class, ISectionRelation;
}
