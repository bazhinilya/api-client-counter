using api_client_counter.Models.Dto.Request;
using api_client_counter.Service;
using Microsoft.AspNetCore.Mvc;

namespace api_client_counter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {

        private readonly ILogger<AppController> _logger;
        private readonly AppService _applicationService;

        public AppController(ILogger<AppController> logger, Db.AppContext applicationContext)
        {
            _applicationService = new AppService(applicationContext);
            _logger = logger;
        }

        [HttpGet(nameof(GetClients))]
        public async Task<IActionResult> GetClients() => Ok(await _applicationService.GetClients());

        [HttpGet(nameof(GetClientByInn))]
        public async Task<IActionResult> GetClientByInn(long inn) => Ok(await _applicationService.GetClient(inn));

        [HttpPost(nameof(PostClient))]
        public async Task<IActionResult> PostClient(RequestClientDto clientDto)
        {
            await _applicationService.AddClient(clientDto);
            return Ok("Success added");
        }

        [HttpPut(nameof(PutClient))]
        public async Task<IActionResult> PutClient(long currentInn, RequestClientDto newClient) => 
           Ok(await _applicationService.UpdateClient(currentInn, newClient));

        [HttpDelete(nameof(DeleteUsers))]
        public async Task<IActionResult> DeleteUsers(long[] inn)
        {
            await _applicationService.RemoveClients(inn);
            return Ok("Success deleted");
        }
    }
}