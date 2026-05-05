using Microsoft.AspNetCore.Mvc;
using Record_Shop_Backend.MVC_Services;

namespace Record_Shop_Backend.MVC_Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
    }
}
