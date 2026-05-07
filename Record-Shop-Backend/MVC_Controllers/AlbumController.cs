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
            if (_albumService.FetchAllAlbums() == null)
            {
                return NoContent();
            }
            return Ok(_albumService.FetchAllAlbums());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAlbumById(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Please use a positive int as an ID");
            }
            if(_albumService.FetchAlbumById(id) == null)
            {
                return NotFound("No album has been registered with this ID");
            }
            return Ok(_albumService.FetchAlbumById(id));
        }


        //POST
        [HttpPost]
        public IActionResult PostAlbum(Album album)
        {
            if (!ModelState.IsValid || _albumService.SendAlbum(album) == null)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtAction(nameof(PostAlbum), _albumService.SendAlbum(album));
        }

        //PUT
        [HttpPut]
        public IActionResult PutAlbum(Album album)
        {
            if (!ModelState.IsValid || _albumService.UpdateAlbum(album) == null)
            {
                return BadRequest(ModelState);
            }
            if (_albumService.UpdateAlbum(album).Name == "this album has been created")
            {
                return CreatedAtAction(nameof(PutAlbum), _albumService.UpdateAlbum(album));
            }
            return Ok(_albumService.UpdateAlbum(album));
        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteAlbum(int id)
        {
            if (_albumService.RemoveAlbum(id) == null)
            {
                return BadRequest($"No album exists with the ID: {id}");
            }
            return Ok(_albumService.RemoveAlbum(id));
        }
    }
}
