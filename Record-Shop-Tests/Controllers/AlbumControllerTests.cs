using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                Artist = "Eden",
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
                Artist = "Eden",
                ReleaseYear = "2015",
                Genre = "Electronic / Alt-Pop",
                Price = 12.99,
                Stock = 25
            };
            Album ityttmom = new Album
            {
                AlbumId = 2,
                Name = "i think you think too much of me",
                Artist = "Eden",
                ReleaseYear = "2016",
                Genre = "Indie Pop / Rock",
                Price = 14.99,
                Stock = 15
            };
            Album dark = new Album
            {
                AlbumId = 3,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var albumList = new List<Album>() { endCredits, ityttmom, dark };
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
                Artist = "Eden",
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
        public void PostAlbum_ReturnsCreated_WhenInputValid()
        {
            //Arrange
            Album album = new Album
            {
                AlbumId = 3,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            _albumServiceMock.Setup(service => service.SendAlbum(album)).Returns(album);

            //Act
            var result = _albumController.PostAlbum(album);
            var IActionResult = (CreatedAtActionResult)result;

            //Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        [TestCase("Name")]
        [TestCase("Artist")]
        [TestCase("ReleaseYear")]
        [TestCase("Genre")]
        [TestCase("Price")]
        [TestCase("Stock")]
        public void PostAlbum_ReturnsBadRequest_WhenMissingInput(string propertyName)
        {
            //Arrange
            _albumController.ModelState.AddModelError(propertyName, $"{propertyName} Is Required");

            //Act
            var result = _albumController.PostAlbum(new Album());
            var IActionResult = (BadRequestObjectResult)result;
            var errors = (SerializableError)IActionResult.Value;
            var errorMessages = (string[])errors[propertyName];

            //Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Console.WriteLine(errorMessages[0]);
        }

        [Test]
        public void PutAlbum_ReturnsOk_WhenInputValidAndExistsOnDb()
        {
            //Arrange
            Album album = new Album
            {
                AlbumId = 3,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            _albumServiceMock.Setup(service => service.UpdateAlbum(album)).Returns(album);

            //Act
            var result = _albumController.PutAlbum(album);
            var IActionResult = (OkObjectResult)result;

            //Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void PutAlbum_ReturnsCreated_WhenInputValidButNotExistsOnDb()
        {
            //Arrange
            Album album = new Album
            {
                AlbumId = 3,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var newAlbum = album;
            newAlbum.Name = "this album has been created";
            _albumServiceMock.Setup(service => service.UpdateAlbum(album)).Returns(newAlbum);

            //Act
            var result = _albumController.PutAlbum(album);
            var IActionResult = (CreatedAtActionResult)result;

            //Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        [TestCase("Name")]
        [TestCase("Artist")]
        [TestCase("ReleaseYear")]
        [TestCase("Genre")]
        [TestCase("Price")]
        [TestCase("Stock")]
        public void PutAlbum_ReturnsBadRequest_WhenMissingInput(string propertyName)
        {
            //Arrange
            _albumController.ModelState.AddModelError(propertyName, $"{propertyName} Is Required");

            //Act
            var result = _albumController.PutAlbum(new Album());
            var IActionResult = (BadRequestObjectResult)result;
            var errors = (SerializableError)IActionResult.Value;
            var errorMessages = (string[])errors[propertyName];

            //Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Console.WriteLine(errorMessages[0]);
        }
    }
}
