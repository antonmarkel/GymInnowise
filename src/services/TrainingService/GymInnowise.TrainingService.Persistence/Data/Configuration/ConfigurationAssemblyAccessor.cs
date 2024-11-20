using System.Reflection;

namespace GymInnowise.TrainingService.Persistence.Data.Configuration
{
    public static class ConfigurationAssemblyAccessor
    {
        public static readonly Assembly Assembly = typeof(ConfigurationAssemblyAccessor).Assembly;
    }
}
