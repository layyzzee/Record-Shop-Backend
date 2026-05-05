using Record_Shop_Backend.MVC_Controllers;
using Record_Shop_Backend.MVC_Services;
using Moq;

namespace Record_Shop_Tests.Controllers
{
    public class Tests
    {
        private Mock<IAlbumService> _albumServiceMock;
        private AlbumController _albumController;

        [SetUp]
        public void Setup()
        {
            _albumServiceMock = new Mock<IAlbumService>();
            _albumController = new AlbumController(_albumServiceMock.Object);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}