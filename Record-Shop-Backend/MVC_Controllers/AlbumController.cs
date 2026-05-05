using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        //GET METHODS
        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            var albums = _albumService.FetchAllAlbums();
            if (albums == null)
            {
                return NoContent();
            }
            return Ok(albums);
        }
    }
}
