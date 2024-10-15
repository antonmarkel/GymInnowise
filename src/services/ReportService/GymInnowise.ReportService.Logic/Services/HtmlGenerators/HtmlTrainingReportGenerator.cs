using GymInnowise.ReportService.Configuration.Settings;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.Shared.Reports;
using Microsoft.Extensions.Options;
using Razor.Templating.Core;

namespace GymInnowise.ReportService.Logic.Services.HtmlGenerators
{
    public class HtmlTrainingReportGenerator : IHtmlReportGenerator<TrainingReport>

    {
        private readonly ReportViewSettings _viewSettings;

        public HtmlTrainingReportGenerator(IOptions<ReportViewSettings> viewSettings)
        {
            _viewSettings = viewSettings.Value;
        }

        public async Task<string> GenerateHtmlAsync(TrainingReport report)
        {
            var html =
                await RazorTemplateEngine.RenderAsync(Path.Combine(_viewSettings.BaseViewPath, nameof(TrainingReport)));

            return html;
        }
    }
}
