using GymInnowise.TrainingService.Persistence.Entities.Trainings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class SectionTrainingEntityConfiguration : TrainingEntityBaseConfiguration<SectionTrainingEntity>
    {
        public override void Configure(EntityTypeBuilder<SectionTrainingEntity> builder)
        {
            base.Configure(builder);
            builder.HasOne(ent => ent.Section);
            builder.ToTable("SectionTrainings");
        }
    }
}