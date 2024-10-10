namespace GymInnowise.ReportService.Perstistence.Models.Base
{
    public abstract class ReportEntityBase
    {
        public Guid Id { get; set; }
        public Guid? FileId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
