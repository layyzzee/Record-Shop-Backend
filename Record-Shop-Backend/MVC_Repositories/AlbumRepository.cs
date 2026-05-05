using Record_Shop_Backend.MVC_Data_Models;

namespace Record_Shop_Backend.MVC_Repositories
{
    public interface IAlbumRepository
    {
        public IEnumerable<Album> GrabAllAlbums();
    }
    public class AlbumRepository : IAlbumRepository
    {
        public IEnumerable<Album> GrabAllAlbums()
        {
            return null;
        }

    }
}
