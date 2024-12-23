﻿
namespace GymInnowise.Shared.Sections.Base
{
    public abstract class SectionBase
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal CostPerTraining { get; set; }
        public bool IsActive { get; set; }
        public string[] Tags { get; set; } = [];
        public Guid? ThumbnailId { get; set; }
    }
}
