using GymInnowise.GymService.Persistence.Models.Entities;
using GymInnowise.Shared.Gym.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using Newtonsoft.Json;

namespace GymInnowise.GymService.Persistence.Data.Configuration
{
    public class GymEntityTypeConfiguration : IEntityTypeConfiguration<GymEntity>
    {
        public void Configure(EntityTypeBuilder<GymEntity> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).HasMaxLength(100).IsRequired();
            builder.Property(g => g.Address).HasMaxLength(200).IsRequired();
            builder.Property(g => g.ContactInfo).HasMaxLength(200).IsRequired();
            builder.Property(g => g.UsageType).HasConversion<string>().IsRequired();
            builder.Property(g => g.PayType).HasConversion<string>().IsRequired();
            builder.Property(g => g.Tags).HasConversion(GetGymTagConverter());
        }

        private static ValueConverter<List<GymTag>, string> GetGymTagConverter()
        {
            return new ValueConverter<List<GymTag>, string>(
                v => JsonConvert.SerializeObject(
                    v.Select(v => v.ToString())),
                v => JsonConvert.DeserializeObject<List<string>>(v)!
                    .Select(s => (GymTag)Enum.Parse(typeof(GymTag), s)).ToList());
        }
    }
}
