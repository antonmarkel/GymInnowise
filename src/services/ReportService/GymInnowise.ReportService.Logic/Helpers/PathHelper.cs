using System.Text;

namespace GymInnowise.ReportService.Logic.Helpers
{
    public static class PathHelper
    {
        public static string GetViewPath(string basePath, Type viewType)
        {
            var viewPathBuilder = new StringBuilder();
            viewPathBuilder.Append('/');
            viewPathBuilder.Append(basePath);
            viewPathBuilder.Append('/');
            viewPathBuilder.Append(viewType.Name);
            viewPathBuilder.Append(".cshtml");

            return viewPathBuilder.ToString();
        }
    }
}
