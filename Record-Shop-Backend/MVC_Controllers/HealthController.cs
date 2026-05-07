using Microsoft.AspNetCore.Mvc;

namespace Record_Shop_Backend.MVC_Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly AlbumHealthCheck _albumHealthCheck;

        public HealthController(AlbumHealthCheck albumHealthCheck)
        {
            _albumHealthCheck = albumHealthCheck;
        }

        [HttpGet]
        public AlbumHealthCheck GetHealthCheck()
        {
            return _albumHealthCheck;
        }
    }
}
