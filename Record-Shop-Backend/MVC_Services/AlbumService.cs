using Record_Shop_Backend.MVC_Repositories;
using Record_Shop_Backend.MVC_Data_Models;
using Microsoft.IdentityModel.Tokens;

namespace Record_Shop_Backend.MVC_Services
{
    public interface IAlbumService
    {
        public IEnumerable<Album>? FetchAllAlbums();
        public Album? FetchAlbumById(int id);
    }
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        //GET METHODS
        public IEnumerable<Album>? FetchAllAlbums()
        {
            var albums = _albumRepository.GrabAllAlbums();
            if (albums == null || !albums.Any()) //returning null if null OR empty
            {
                return null;
            }
            return albums;
        }
        public Album? FetchAlbumById(int id)
        {
            return null;
        }

    }
}
