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
        public IEnumerable<Album>? GrabAlbumByArtist(string artist);
        public IEnumerable<Album>? GrabAlbumByReleaseYear(int releaseYear);
        public IEnumerable<Album>? GrabAlbumByGenre(string genre);
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

        public IEnumerable<Album>? GrabAlbumByArtist(string artist)
        {
            var albumsByArtist = _context.Albums.Where(a => a.Artist.ToLower() == artist.ToLower()).ToList();
            if (albumsByArtist == null)
            {
                return null;
            }
            return albumsByArtist;
        }

        public IEnumerable<Album>? GrabAlbumByReleaseYear(int releaseYear)
        {
            var albumsbyYear = _context.Albums.Where(a => a.ReleaseYear == releaseYear.ToString());
            if (albumsbyYear == null)
            {
                return null;
            }
            return albumsbyYear;
        }

        public IEnumerable<Album>? GrabAlbumByGenre(string genre)
        {
            var albumsbyGenre = _context.Albums.Where(a => a.Genre.Contains(genre)).ToList();
            if (albumsbyGenre == null)
            {
                return null;
            }
            return albumsbyGenre;
        }

        //POST METHODS
        public Album? SubmitAlbum(Album album)
        {
            var exists = _context.Albums.Any(a => a.Name == album.Name && a.Artist == album.Artist);
            if (!exists)
            {
                album.AlbumId = 0;
                _context.Albums.Add(album);
                _context.SaveChanges();
                return album;
            }
            return null;
        }

        //PUT METHODS
        public Album? AlterAlbum(Album album)
        {
            var existingAlbum = _context.Albums.FirstOrDefault(a => a.AlbumId == album.AlbumId);
            if(existingAlbum != null)
            {
                existingAlbum.Name = album.Name;
                existingAlbum.Artist = album.Artist;
                existingAlbum.ReleaseYear = album.ReleaseYear;
                existingAlbum.Genre = album.Genre;
                existingAlbum.Price = album.Price;
                existingAlbum.Stock = album.Stock;
                existingAlbum.ImageUrl = album.ImageUrl;
                _context.SaveChanges();
                return album;
            }
            else
            {
                return null;
            }
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
