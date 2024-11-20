﻿namespace GymInnowise.TrainingService.Persistence.Entities.Redundant
{
    public class SectionEntity
    {
        public Guid SectionId { get; set; }
        public required string Name { get; set; }
        public Guid? ThumbnailId { get; set; }
    }
}
