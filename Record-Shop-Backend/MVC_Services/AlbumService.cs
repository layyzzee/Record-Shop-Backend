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
            try
            {
                var albums = _albumRepository.GrabAllAlbums();
                if (albums == null || !albums.Any())
                {
                    return null;
                }
                return albums;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Album? FetchAlbumById(int id)
        {
            try
            {
                var album = _albumRepository.GrabAlbumById(id);
                if (album == null || string.IsNullOrEmpty(album.Name))
                {
                    return null;
                }
                return album;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Album? SendAlbum(Album album)
        {
            try
            {
                return _albumRepository.SubmitAlbum(album);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
        public Album? UpdateAlbum(Album album)
        {
            try
            {
                return _albumRepository.AlterAlbum(album);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
