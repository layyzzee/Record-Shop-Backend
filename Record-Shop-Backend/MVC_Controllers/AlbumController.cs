using Microsoft.AspNetCore.Mvc;
using Record_Shop_Backend.MVC_Services;
using Record_Shop_Backend.MVC_Data_Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAlbumById(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Please use a positive int as an ID");
            }
            var album = _albumService.FetchAlbumById(id);
            if(album == null)
            {
                return NotFound("No album has been registered with this ID");
            }
            return Ok(album);
        }


        //POST
        [HttpPost]
        public IActionResult PostAlbum([FromBody]Album album)
        {
            var newAlbum = _albumService.SendAlbum(album);
            if (!ModelState.IsValid || newAlbum == null) return BadRequest(ModelState);
            return CreatedAtAction(nameof(PostAlbum), newAlbum);
        }

        //PUT
        [HttpPut]
        public IActionResult PutAlbum([FromBody] Album album)
        {
            var newAlbum = _albumService.UpdateAlbum(album);
            if (!ModelState.IsValid || newAlbum == null) return BadRequest(ModelState);
            if(newAlbum.Name == "this album has been created") return CreatedAtAction(nameof(PutAlbum), newAlbum);
            return Ok(newAlbum);
        }
    }
}
