using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Honeypot.Controllers
{
    [ApiController]
    [Route("/{**slug}")]
    public class HoneypotController : ControllerBase
    {

        private readonly ILogger<HoneypotController> _logger;

        public HoneypotController(ILogger<HoneypotController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task Get()
        {
            _logger.LogInformation(Request.Host.ToUriComponent());
            Response.StatusCode = 200;
            while (true)
            {
                Thread.Sleep(5000);
                _ = Task.Factory.StartNew(async () =>
                {
                    byte[] bytes = Encoding.ASCII.GetBytes("Not found\n");
                    var outputStream = Response.Body;
                    await outputStream.WriteAsync(bytes);
                    await outputStream.FlushAsync();
                }, TaskCreationOptions.LongRunning);
            }
        }
    }
}