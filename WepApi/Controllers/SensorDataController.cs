using Microsoft.AspNetCore.Mvc;
using WepApi.Auth;
using WepApi.Model;

namespace WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorDataController : Controller
    {

        private readonly ILogger<SensorDataController> _logger;
        private IApiKeyValidation _apiKeyValidation;
        private const string _connection = "ws://127.0.0.1:9000/Sensor";
        public SensorDataController(ILogger<SensorDataController> logger, IApiKeyValidation apiKey)
        {
            _logger = logger;
            _apiKeyValidation = apiKey;
        }

        [HttpGet(Name = "GetSensorData")]
        public IActionResult GetSensorData()
        {
            string? userApiKey = Request.Headers[Constants.apiKeyHeaderName];

            bool isValid = _apiKeyValidation.IsValid(userApiKey);
            if (!isValid ) { return Unauthorized(); }
            var jsonData = new { connectionString = _connection};
            return Ok(jsonData);
        }

        [HttpPost(Name ="SetSensorRefreshPeriod")]
        public IActionResult SetSensorData([FromBody] RefreshModel refreshModel)
        {
            string? userApiKey = Request.Headers[Constants.apiKeyHeaderName];
            
            bool isValid = _apiKeyValidation.IsValid(userApiKey);
            if (!isValid) { return Unauthorized(); }
            if (refreshModel == null || refreshModel.RefreshFreq <= 0)
            {
                return BadRequest("Refresh must be interger positive number");
            }
               
            return Ok();
        }
    }
}
