using System.ComponentModel.DataAnnotations;

namespace GymInnowise.ReportService.Perstistence.Models.Interfaces
{
    public interface IReportEntity
    {
        [Key] public Guid Id { get; set; }
    }
}
