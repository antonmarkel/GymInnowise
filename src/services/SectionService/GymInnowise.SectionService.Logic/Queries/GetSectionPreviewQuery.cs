using GymInnowise.Shared.Sections.Base;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Queries
{
    public sealed record GetSectionPreviewQuery(Guid SectionId) : IRequest<OneOf<SectionBase, NotFound>>;
}