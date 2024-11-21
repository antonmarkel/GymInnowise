using GymInnowise.Shared.Sections.Base;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Commands
{
    public sealed record UpdateSectionCommand(Guid SectionId, SectionBase UpdateData)
        : IRequest<OneOf<Success, NotFound>>;
}
