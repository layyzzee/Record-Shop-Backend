using Microsoft.IdentityModel.Tokens;
using Record_Shop_Backend.Data;
using Record_Shop_Backend.MVC_Data_Models;

namespace Record_Shop_Backend.MVC_Repositories
{
    public interface IAlbumRepository
    {
        public IEnumerable<Album>? GrabAllAlbums();
        public Album? GrabAlbumById(int id);
        public Album? SubmitAlbum(Album album);
        public Album? AlterAlbum(Album album);


    }
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AlbumDbContext _context;
        public AlbumRepository(AlbumDbContext albumDb)
        {
            _context = albumDb;
        }

        public IEnumerable<Album>? GrabAllAlbums()
        {
            return _context.Albums.ToList() ?? new List<Album>();
        }

        public Album? GrabAlbumById(int id)
        {
            return _context.Albums.FirstOrDefault(album => album.AlbumId == id);
        }

        public Album? SubmitAlbum(Album album)
        {
            _context.Albums.Add(album);
            return album; 
        }

        public Album? AlterAlbum(Album album)
        {
            return null;
        }

    }
}
