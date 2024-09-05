using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//
namespace JWT.Controllers
{
    ///
    //1
    //3
    //5
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login([FromBody] UserDto userDto)
        {
            AuthenticateService service = new AuthenticateService();
            var token = service.Authenticate();
            //////userDto = service.GetUserDataForLogin();

            return Ok(new
            {
                Response = "Successful",
                Token = token,
                Data = userDto
            });
        }
    }


}
//2