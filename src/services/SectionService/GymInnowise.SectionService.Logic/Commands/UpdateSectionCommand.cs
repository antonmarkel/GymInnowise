using GymInnowise.Shared.Sections.Base;
using MediatR;

namespace GymInnowise.SectionService.Logic.Commands
{
    public sealed record UpdateSectionCommand(Guid SectionId, SectionBase UpdateData) : IRequest;
}
