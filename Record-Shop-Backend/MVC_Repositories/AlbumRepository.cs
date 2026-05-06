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
        public Album? DestroyAlbum(int id);


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
            int holder = album.AlbumId;
            album.AlbumId = 0;
            _context.Albums.Add(album);
            _context.SaveChanges();
            album.AlbumId = holder;
            return album;
        }

        public Album? AlterAlbum(Album album)
        {
            int holder = album.AlbumId;
            album.AlbumId = 0;
            var existingAlbum = _context.Albums.FirstOrDefault(a => a.Name == album.Name && a.Artist == album.Artist);
            if(existingAlbum != null)
            {
                existingAlbum.ReleaseYear = album.ReleaseYear;
                existingAlbum.Genre = album.Genre;
                existingAlbum.Price = album.Price;
                existingAlbum.Stock = album.Stock;
                _context.SaveChanges();
            }
            else
            {
                _context.Albums.Add(album);
                _context.SaveChanges();
            }
            album.AlbumId = holder;
            album.Name = "this album has been created";
            return album;
        }

        public Album? DestroyAlbum(int id)
        {
            var album = _context.Albums.FirstOrDefault(album => album.AlbumId == id);
            var newAlbum = new Album();
            newAlbum.Name = "this album doesn't exist";
            if(album != null)
            {
                _context.Albums.Remove(album);
                _context.SaveChanges();
                return album;
            }
            else
            {
                return newAlbum;
            }
        }

    }
}
