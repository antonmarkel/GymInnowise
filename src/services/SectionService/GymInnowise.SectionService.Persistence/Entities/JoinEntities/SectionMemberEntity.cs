using GymInnowise.SectionService.Persistence.Entities.Base;

namespace GymInnowise.SectionService.Persistence.Entities.JoinEntities
{
    public class SectionMemberEntity : IJoinEntity
    {
        public Guid SectionId { get; set; }
        public SectionEntity? Section { get; set; }
        public Guid MemberId { get; set; }
        public ProfileEntity? Member { get; set; }
        public DateTime DateJoinedUtc { get; set; }
    }
}
