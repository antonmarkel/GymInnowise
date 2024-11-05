using GymInnowise.Shared.Sections.Dtos.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Queries
{
    public sealed record GetSectionFullQuery(Guid SectionId) : IRequest<OneOf<GetSectionFullResponse, NotFound>>;
}
