using GymInnowise.UserService.Persistence.Models.Abstract;

namespace GymInnowise.UserService.Persistence.Models
{
    public class ClientProfile : Profile
    {
        public override string GetTableName()
        {
            return "ClientProfiles";
        }
    }
}
