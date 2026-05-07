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
            if (albums == null)
            {
                return null;
            }
            return albums;
        }

        public Album? FetchAlbumById(int id)
        {
            var albums = _albumRepository.GrabAlbumById(id);
            if (albums == null)
            {
                throw new ArgumentNullException("Album ID doesnt exist on the database");
            }
            return albums;
        
        }

        public Album? SendAlbum(Album album)
        {
            var albums = _albumRepository.SubmitAlbum(album);
            if (albums == null)
            {
                throw new ArgumentException("Album already exists in the database");
            }
            return albums;
        }

        public Album? UpdateAlbum(Album album)
        {
            var albums = _albumRepository.AlterAlbum(album);
            return albums;
        }

        public Album? RemoveAlbum(int id)
        {
            var albums = _albumRepository.DestroyAlbum(id);
            return albums;
        }

    }
}
