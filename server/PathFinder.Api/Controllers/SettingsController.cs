using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PathFinder.Api.Models;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("settings")]
    public class SettingsController : Controller
    {
        private readonly SettingsProvider settingsProvider;
        public SettingsController(SettingsProvider settingsProvider)
        {
            this.settingsProvider = settingsProvider;
        }

        [HttpGet]
        public ActionResult<string> GetSettings()
        {
            return JsonConvert.SerializeObject(settingsProvider.GetSettings(), Formatting.Indented);
        }
    }
}