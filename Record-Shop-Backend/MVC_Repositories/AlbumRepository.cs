using Microsoft.IdentityModel.Tokens;
using Record_Shop_Backend.Data;
using Record_Shop_Backend.MVC_Data_Models;

namespace Record_Shop_Backend.MVC_Repositories
{
    public interface IAlbumRepository
    {
        public IEnumerable<Album>? GrabAllAlbumsInStock();
        public Album? GrabAlbumById(int id);
        public Album? SubmitAlbum(Album album);
        public Album? AlterAlbum(Album album);
        public Album? DestroyAlbum(int id);
        public Album? GrabAlbumByArtist(string artist);


    }
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AlbumDbContext _context;
        public AlbumRepository(AlbumDbContext albumDb)
        {
            _context = albumDb;
        }

        //GET METHODS
        public IEnumerable<Album>? GrabAllAlbumsInStock()
        {
            var albumsInStock = _context.Albums.Where(albums => albums.Stock > 0).ToList();
            if (albumsInStock == null)
            {
                return null;
            }
            return albumsInStock;
        }

        public Album? GrabAlbumById(int id)
        {
            return _context.Albums.FirstOrDefault(album => album.AlbumId == id);
        }

        public Album? GrabAlbumByArtist(string artist)
        {
            var albumsByArtist = _context.Albums.FirstOrDefault(a => a.Artist == artist);
            if (albumsByArtist == null)
            {
                return null;
            }
            return albumsByArtist;
        }

        //POST METHODS
        public Album? SubmitAlbum(Album album)
        {
            var exists = _context.Albums.Any(a => a.Name == album.Name && a.Artist == album.Artist);
            if (!exists)
            {
                _context.Albums.Add(album);
                _context.SaveChanges();
                return album;
            }
            return null;
        }

        //PUT METHODS
        public Album? AlterAlbum(Album album)
        {
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
                album.AlbumId = 0;
                _context.Albums.Add(album);
                _context.SaveChanges();
                album.Name = "this album has been created";
            }
            return album;
        }

        //DELETE METHODS
        public Album? DestroyAlbum(int id)
        {
            var album = _context.Albums.FirstOrDefault(album => album.AlbumId == id);
            if(album != null)
            {
                _context.Albums.Remove(album);
                _context.SaveChanges();
                return album;
            }
            else
            {
                return null;
            }
        }

    }
}
