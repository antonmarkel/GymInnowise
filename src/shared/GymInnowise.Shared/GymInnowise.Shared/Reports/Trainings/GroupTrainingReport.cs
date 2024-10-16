using GymInnowise.Shared.Reports.Base;

namespace GymInnowise.Shared.Reports.Trainings
{
    public class GroupTrainingReport : TrainingReportBase
    {
        public string[] Coaches { get; set; } = [];
        public string[] Participants { get; set; } = [];
        public string[] Absent { get; set; } = [];
    }
}
