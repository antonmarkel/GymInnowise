using GymInnowise.Shared.Sections.Base;
using MediatR;

namespace GymInnowise.SectionService.Logic.Queries
{
    public sealed record GetSectionsByTagsQuery(string[]? Tags) : IRequest<IReadOnlyList<SectionBase>>;
}
