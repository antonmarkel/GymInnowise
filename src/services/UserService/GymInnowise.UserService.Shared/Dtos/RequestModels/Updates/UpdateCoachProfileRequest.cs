namespace GymInnowise.UserService.Shared.Dtos.RequestModels.Updates
{
    public class UpdateCoachProfileRequest : UpdateClientProfileRequest
    {
        public decimal CostPerHour { get; set; }
    }
}
