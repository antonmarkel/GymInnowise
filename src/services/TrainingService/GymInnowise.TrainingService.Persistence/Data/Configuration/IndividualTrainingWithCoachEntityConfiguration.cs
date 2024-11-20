using GymInnowise.TrainingService.Persistence.Entities.Trainings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class IndividualTrainingWithCoachEntityConfiguration
        : IEntityTypeConfiguration<IndividualWithCoachTrainingEntity>
    {
        public void Configure(EntityTypeBuilder<IndividualWithCoachTrainingEntity> builder)
        {
            builder.HasOne(ent => ent.Account);
            builder.HasOne(ent => ent.Coach);
        }
    }
}
