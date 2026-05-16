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
        public IActionResult GetAllAlbumsInStock()
        {
            var albums = _albumService.FetchAllAlbumsInStock();
            if (albums == null)
            {
                return NoContent();
            }
            return Ok(albums);
        }

        [HttpGet]
        [Route("id/{id:int}")]
        public IActionResult GetAlbumById(int id)
        {
            var albums = _albumService.FetchAlbumById(id);
            if (id <= 0)
            {
                return BadRequest("Please use a positive int as an ID");
            }
            if (albums == null)
            {
                return NotFound($"No album has been registered with this ID: {id}");
            }
            return Ok(albums);
        }

        [HttpGet]
        [Route("artist/{artist}")]
        public IActionResult GetAlbumByArtist(string artist)
        {
            var albums = _albumService.FetchAlbumByArtist(artist);
            if (albums.Count() == 0)
            {
                return NotFound($"No album's have been registered from the artist: {artist}");
            }
            return Ok(albums);
        }

        [HttpGet]
        [Route("year/{releaseYear:int}")]
        public IActionResult GetAlbumByReleaseYear(int releaseYear)
        {
            var albums = _albumService.FetchAlbumByReleaseYear(releaseYear);
            if (albums.Count() == 0)
            {
                return NotFound($"No album's have been registered from the year: {releaseYear}");
            }
            return Ok(albums);
        }


        //POST
        [HttpPost]
        public IActionResult PostAlbum(Album album)
        {
            var albums = _albumService.SendAlbum(album);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(albums == null)
            {
                return Conflict(albums);
            }
            return CreatedAtAction(nameof(PostAlbum), albums);
        }

        //PUT
        [HttpPut]
        public IActionResult PutAlbum(Album album)
        {
            var albums = _albumService.UpdateAlbum(album);
            if (!ModelState.IsValid || albums == null)
            {
                return BadRequest(ModelState);
            }
            if (albums.Name == "this album has been created")
            {
                return CreatedAtAction(nameof(PutAlbum), albums);
            }
            return Ok(albums);
        }

        //Delete
        [HttpDelete("{id:int}")]
        public IActionResult DeleteAlbum(int id)
        {
            var albums = _albumService.RemoveAlbum(id);
            if (albums == null)
            {
                return BadRequest($"No album exists with the ID: {id}");
            }
            return Ok(albums);
        }
    }
}
