using Microsoft.AspNetCore.Mvc;
using Moq;
using Record_Shop_Backend.MVC_Controllers;
using Record_Shop_Backend.MVC_Data_Models;
using Record_Shop_Backend.MVC_Services;

namespace Record_Shop_Tests.Controllers
{
    public class AlbumControllerTests
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
        public void GetAllAlbums_ReturnsNoContent_WhenNullInput()
        {
            //Arrange
            List<Album>? albumList = null;
            _albumServiceMock.Setup(service => service.FetchAllAlbums()).Returns(albumList);

            //Act
            var result = _albumController.GetAllAlbums();

            //Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void GetAllAlbums_ReturnsAlbumList_WhenSingleInput()
        {
            //Arrange
            Album endCredits = new Album
            {
                AlbumId = 1,
                Name = "End Credits",
                ReleaseYear = "2015",
                Genre = "Electronic / Alt-Pop",
                Price = 12.99,
                Stock = 25
            };
            var albumList = new List<Album>() { endCredits };
            _albumServiceMock.Setup(service => service.FetchAllAlbums()).Returns(albumList);

            //Act
            var result = _albumController.GetAllAlbums();
            var IActionResult = (OkObjectResult)result;
            var myAlbums = (IEnumerable<Album>)IActionResult.Value;

            //Assert
            Assert.That(albumList, Is.EquivalentTo(myAlbums));
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetAllAlbums_ReturnsMyAlbums_WhenMultipleAlbums()
        {
            //Arrange
            Album endCredits = new Album
            {
                AlbumId = 1,
                Name = "End Credits",
                ReleaseYear = "2015",
                Genre = "Electronic / Alt-Pop",
                Price = 12.99,
                Stock = 25
            };
            Album ityttmom = new Album
            {
                AlbumId = 2,
                Name = "i think you think too much of me",
                ReleaseYear = "2016",
                Genre = "Indie Pop / Rock",
                Price = 14.99,
                Stock = 15
            };
            Album dark = new Album
            {
                AlbumId = 3,
                Name = "Dark",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var albumList = new List<Album>() { endCredits, ityttmom, dark};
            _albumServiceMock.Setup(service => service.FetchAllAlbums()).Returns(albumList);

            //Act
            var result = _albumController.GetAllAlbums();
            var IActionResult = (OkObjectResult)result;
            var myAlbums = (IEnumerable<Album>)IActionResult.Value;

            //Assert
            Assert.That(albumList, Is.EquivalentTo(myAlbums));
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }


        [Test]
        public void GetAlbumById_ReturnsNotFound_WhenIdNotExistsInDb()
        {
            //Arrange
            int id = 2;

            //Act
            var result = _albumController.GetAlbumById(id);

            //Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
        [Test]
        public void GetAlbumById_ReturnsOk_WhenValidRequest()
        {
            //Arrange
            Album dark = new Album
            {
                AlbumId = 3,
                Name = "Dark",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var id = 3;
            _albumServiceMock.Setup(service => service.FetchAlbumById(id)).Returns(dark);


            //Act
            var result = _albumController.GetAlbumById(id);
            var IActionResult = (OkObjectResult)result;
            var myAlbums = (Album)IActionResult.Value;

            //Assert
            var expected = dark;
            Assert.That(IActionResult.Value, Is.EqualTo(expected));
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void GetAlbumById_ReturnsBadRequest_WhenIdInvalid()
        {
            //Arrange
            int id = -2;

            //Act
            var result = _albumController.GetAlbumById(id);

            //Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }


        [Test]
        public void PostAlbum_ReturnsAlbum_WhenInputValid()
        {
            //Arrange
            Album dark = new Album
            {
                AlbumId = 3,
                Name = "Dark",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var album = dark;

            //Act
            var result = _albumController.PostAlbum(album);
            var IActionResult = (OkObjectResult)result;

            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(IActionResult.Value, Is.EqualTo(dark));
        }
        [Test]
        public void PostAlbum_ReturnsBadRequest_WhenInputNoName()
        {
            //Arrange
            Album dark = new Album
            {
                AlbumId = 3,
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var album = dark;

            //Act
            var result = _albumController.PostAlbum(album);
            var IActionResult = (BadRequestObjectResult)result;

            //Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(IActionResult.Value, Is.EqualTo("Album Name must be present"));
        }
    }
}