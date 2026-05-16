using Record_Shop_Backend.MVC_Repositories;
using Record_Shop_Backend.MVC_Data_Models;
using Microsoft.IdentityModel.Tokens;

namespace Record_Shop_Backend.MVC_Services
{
    public interface IAlbumService
    {
        public IEnumerable<Album>? FetchAllAlbumsInStock();
        public Album? FetchAlbumById(int id);
        public Album? SendAlbum(Album album);
        public Album? UpdateAlbum(Album album);
        public Album? RemoveAlbum(int id);
        public Album? FetchAlbumByArtist(string artist);

    }
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        //GET METHODS
        public IEnumerable<Album>? FetchAllAlbumsInStock()
        {
            return  _albumRepository.GrabAllAlbumsInStock();
        }

        public Album? FetchAlbumById(int id)
        {
            return _albumRepository.GrabAlbumById(id);        
        }

        public Album? FetchAlbumByArtist(string artist)
        {
            return _albumRepository.GrabAlbumByArtist(artist);
        }

        //POST METHODS
        public Album? SendAlbum(Album album)
        {
            return _albumRepository.SubmitAlbum(album);
        }

        //PUT METHODS
        public Album? UpdateAlbum(Album album)
        {
            return _albumRepository.AlterAlbum(album);
        }

        //DELETE METHODS
        public Album? RemoveAlbum(int id)
        {
            return _albumRepository.DestroyAlbum(id);
        }

    }
}
