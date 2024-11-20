using GymInnowise.TrainingService.Persistence.Entities.Trainings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public class SectionTrainingEntityConfiguration : IEntityTypeConfiguration<SectionTrainingEntity>
    {
        public void Configure(EntityTypeBuilder<SectionTrainingEntity> builder)
        {
            builder.HasOne(ent => ent.Section);
        }
    }
}
