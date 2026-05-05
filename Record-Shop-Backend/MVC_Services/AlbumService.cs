using Record_Shop_Backend.MVC_Repositories;
using Record_Shop_Backend.MVC_Data_Models;
using Microsoft.IdentityModel.Tokens;

namespace Record_Shop_Backend.MVC_Services
{
    public interface IAlbumService
    {
        public IEnumerable<Album>? FetchAllAlbums();
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
            if (_albumRepository.GrabAllAlbums().IsNullOrEmpty()) //Combining Empty and Null for controller
            {
                return null;
            }
            return _albumRepository.GrabAllAlbums();
        }

    }
}
