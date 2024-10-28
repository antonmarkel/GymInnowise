namespace GymInnowise.Shared.User.Dtos.RequestModels.Updates
{
    public class UpdateCoachProfileRequest : UpdateClientProfileRequest
    {
        public decimal CostPerHour { get; set; }
    }
}
