using InteractiveBorderMapApp.Controllers;
using System.IO;

namespace InteractiveBorderMapApp.Services
{
    public class ReportService
    {
        private string webRootPath = HomeController.WebRootPath;
        public FileStream GetReport(string reportId)
        {
            var path = Path.Combine(webRootPath, @$"reports/{reportId}");
            return File.OpenRead(path);
        }
    }
}
