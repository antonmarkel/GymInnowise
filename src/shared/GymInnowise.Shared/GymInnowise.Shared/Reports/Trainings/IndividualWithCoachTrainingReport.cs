using GymInnowise.Shared.Reports.Base;

namespace GymInnowise.Shared.Reports.Trainings
{
    public class IndividualWithCoachTrainingReport : TrainingReportBase
    {
        public required string Coach { get; set; }
    }
}
