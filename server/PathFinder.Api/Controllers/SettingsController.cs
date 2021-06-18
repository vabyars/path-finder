using System.Threading.Tasks;
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
        public async Task<ActionResult<string>> GetSettings()
        {
            var settings = await settingsProvider.GetSettings();
            return JsonConvert.SerializeObject(settings, Formatting.Indented);
        }
    }
}