using System.Reflection;

namespace GymInnowise.Shared.Authorization
{
    public static class Roles
    {
        public const string Client = "Client";
        public const string Coach = "Coach";
        public const string Admin = "Admin";

        public static readonly HashSet<string> AllRoles = GetAllRoles();

        private static HashSet<string> GetAllRoles()
        {
            return typeof(Roles)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(fi => (string)fi.GetRawConstantValue()!)
                .ToHashSet();
        }
    }
}
