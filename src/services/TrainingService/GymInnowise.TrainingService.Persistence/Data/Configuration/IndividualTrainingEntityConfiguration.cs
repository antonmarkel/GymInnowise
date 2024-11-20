using GymInnowise.TrainingService.Persistence.Entities.Trainings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class IndividualTrainingEntityConfiguration :
        TrainingEntityBaseConfiguration<IndividualTrainingEntity>
    {
        public override void Configure(EntityTypeBuilder<IndividualTrainingEntity> builder)
        {
            base.Configure(builder);
            builder.HasOne(ent => ent.Account);
            builder.ToTable("IndividualTrainings");
        }
    }
}