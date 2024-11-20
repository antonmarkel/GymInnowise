using GymInnowise.TrainingService.Persistence.Entities.Trainings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class IndividualTrainingWithCoachEntityConfiguration
        : IEntityTypeConfiguration<IndividualTrainingWithCoachEntity>
    {
        public void Configure(EntityTypeBuilder<IndividualTrainingWithCoachEntity> builder)
        {
            builder.HasOne(ent => ent.Account);
            builder.HasOne(ent => ent.Coach);
        }
    }
}
