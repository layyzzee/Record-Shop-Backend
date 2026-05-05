using Moq;
using Record_Shop_Backend.MVC_Services;
using Record_Shop_Backend.MVC_Repositories;

namespace Record_Shop_Tests.Services
{
    public class Tests
    {
        private Mock<IAlbumRepository> _albumRepositoryMock;
        private AlbumService _albumService;

        [SetUp]
        public void Setup()
        {
            _albumRepositoryMock = new Mock<IAlbumRepository>();
            _albumService = new AlbumService(_albumRepositoryMock.Object);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}