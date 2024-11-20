﻿using GymInnowise.TrainingService.Persistence.Entities.Interfaces;

namespace GymInnowise.TrainingService.Persistence.Entities.Redundant
{
    public class SectionEntity : IRedundantEntity
    {
        public Guid OriginalId { get; set; }
        public required string Name { get; set; }
        public Guid? ThumbnailId { get; set; }
    }
}
