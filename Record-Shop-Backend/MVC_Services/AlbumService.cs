using Record_Shop_Backend.MVC_Repositories;

namespace Record_Shop_Backend.MVC_Services
{
    public interface IAlbumService
    {

    }
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }
    }
}
