using GymInnowise.TrainingService.Persistence.Entities.Trainings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class IndividualTrainingEntityConfiguration :
        IEntityTypeConfiguration<IndividualTrainingEntity>
    {
        public void Configure(EntityTypeBuilder<IndividualTrainingEntity> builder)
        {
            builder.HasOne(ent => ent.Account);
        }
    }
}
