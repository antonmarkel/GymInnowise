using GymInnowise.Shared.Sections.Base;
using MediatR;

namespace GymInnowise.SectionService.Logic.Commands
{
    public sealed record CreateSectionCommand(SectionBase SectionData) : IRequest<Guid>;
}
