using GymInnowise.Shared.Sections.Base;
using GymInnowise.Shared.Sections.SectionRelations.Information;

namespace GymInnowise.Shared.Sections.Dtos.Responses
{
    public class GetSectionFullResponse : SectionBase
    {
        public IReadOnlyList<GymRelationInformation> GymsInfo { get; set; } = [];
        public IReadOnlyList<MembershipInformation> MembersInfo { get; set; } = [];
        public IReadOnlyList<MentorshipInformation> MentorsInfo { get; set; } = [];
    }
}