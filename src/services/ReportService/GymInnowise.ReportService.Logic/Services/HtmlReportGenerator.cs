using GymInnowise.ReportService.Configuration.Settings;
using GymInnowise.ReportService.Logic.Helpers;
using GymInnowise.ReportService.Logic.Interfaces;
using GymInnowise.ReportService.Logic.Results;
using GymInnowise.Shared.Reports.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneOf;
using Razor.Templating.Core;

namespace GymInnowise.ReportService.Logic.Services
{
    public class HtmlReportGenerator<TReport> : IHtmlReportGenerator<TReport> where TReport : IReport
    {
        private readonly ReportViewSettings _viewSettings;
        private readonly ILogger<HtmlReportGenerator<TReport>> _logger;

        public HtmlReportGenerator(IOptions<ReportViewSettings> viewSettings,
            ILogger<HtmlReportGenerator<TReport>> logger)
        {
            _viewSettings = viewSettings.Value;
            _logger = logger;
        }

        public async Task<OneOf<string, HtmlGenerationFailed>> GenerateHtmlAsync(TReport report)
        {
            var viewPath = PathHelper.GetViewPath(_viewSettings.BaseViewPath, typeof(TReport));
            var renderResult = await RazorTemplateEngine.TryRenderPartialAsync(viewPath, report);
            if (!renderResult.ViewExists)
            {
                _logger.LogError("No view was found! @{viewType}", typeof(TReport).Name);

                return new HtmlGenerationFailed();
            }

            var html = renderResult.RenderedView;
            _logger.LogInformation("Html was successfully rendered!");

            return html!;
        }
    }
}