using Record_Shop_Backend.MVC_Repositories;
using Record_Shop_Backend.MVC_Data_Models;
using Microsoft.IdentityModel.Tokens;

namespace Record_Shop_Backend.MVC_Services
{
    public interface IAlbumService
    {
        public IEnumerable<Album>? FetchAllAlbums();
        public Album? FetchAlbumById(int id);
        public Album? SendAlbum(Album album);
        public Album? UpdateAlbum(Album album);
        public Album? RemoveAlbum(int id);

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
            if (_albumRepository.GrabAllAlbums() == null)
            {
                return null;
            }
            return _albumRepository.GrabAllAlbums();
        }

        public Album? FetchAlbumById(int id)
        {
            if (_albumRepository.GrabAlbumById(id) == null)
            {
                throw new ArgumentNullException("Album ID doesnt exist on the database");
            }
            return _albumRepository.GrabAlbumById(id);
        
        }

        public Album? SendAlbum(Album album)
        {
            if(_albumRepository.SubmitAlbum(album) == null)
            {
                throw new ArgumentException("Album already exists in the database");
            }
            return _albumRepository.SubmitAlbum(album);
        }

        public Album? UpdateAlbum(Album album)
        {
            return _albumRepository.AlterAlbum(album);
        }

        public Album? RemoveAlbum(int id)
        {
            return _albumRepository.DestroyAlbum(id);
        }

    }
}
