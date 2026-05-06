using Microsoft.AspNetCore.Mvc;
using Moq;
using Record_Shop_Backend.MVC_Controllers;
using Record_Shop_Backend.MVC_Data_Models;
using Record_Shop_Backend.MVC_Repositories;
using Record_Shop_Backend.MVC_Services;

namespace Record_Shop_Tests.ServicesTests
{
    public class AlbumServiceTests
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
        public void FetchAllAlbums_ReturnsNull_WhenNoAlbums()
        {
            //Assert
            _albumRepositoryMock.Setup(repository => repository.GrabAllAlbums()).Returns(new List<Album>());

            //Act
            var result = _albumService.FetchAllAlbums();

            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void FetchAllAlbums_ReturnsSingle_WithSIngleInput()
        {
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
            _albumRepositoryMock.Setup(repository => repository.GrabAllAlbums()).Returns(albumList);

            //Act
            var result = _albumService.FetchAllAlbums();

            //Assert
            Assert.That(result, Is.EquivalentTo(albumList));
        }

        [Test]
        public void FetchAllAlbums_ReturnsMyAlbums_WhenMultipleAlbums()
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
            _albumRepositoryMock.Setup(repository => repository.GrabAllAlbums()).Returns(albumList);

            //Act
            var result = _albumService.FetchAllAlbums();

            //Assert
            Assert.That(result, Is.EquivalentTo(albumList));
        }
        [Test]
        public void FetchAllAlbums_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            _albumRepositoryMock.Setup(repo => repo.GrabAllAlbums()).Throws(new Exception("Lost connection to the database"));

            // Act
            var result = _albumService.FetchAllAlbums();

            // Assert
            Assert.That(result, Is.Null);
        }
        [Test]
        public void FetchAllAlbums_TimesCalled_Once()
        {
            // Arrange
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
            _albumRepositoryMock.Setup(repo => repo.GrabAllAlbums()).Returns(new List<Album> { dark });

            // Act
            _albumService.FetchAllAlbums();

            // Assert
            _albumRepositoryMock.Verify(repo => repo.GrabAllAlbums(), Times.Once);
        }


        [Test]
        public void FetchAlbumById_ReturnsNull_InputEmpty()
        {
            //Arrange
            int id = 2;
            _albumRepositoryMock.Setup(repository => repository.GrabAlbumById(id)).Returns(new Album());

            //Act
            var result = _albumService.FetchAlbumById(id);

            //Assert
            Assert.That(result, Is.Null);
        }
        [Test]
        public void FetchAlbumById_ReturnsAlbum_InputValid()
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
            int id = 3;
            _albumRepositoryMock.Setup(repository => repository.GrabAlbumById(id)).Returns(dark);

            //Act
            var result = _albumService.FetchAlbumById(id);

            //Assert
            Assert.That(result, Is.EqualTo(dark));
        }
        [Test]
        public void FetchAlbumById_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            int id = 1;
            _albumRepositoryMock.Setup(repo => repo.GrabAlbumById(id)).Throws(new Exception("Lost connection to the database"));

            // Act
            var result = _albumService.FetchAlbumById(id);

            // Assert
            Assert.That(result, Is.Null);
        }
        [Test]
        public void FetchAlbumById_TimesCalled_Once()
        {
            // Arrange
            int testId = 2;
            _albumRepositoryMock.Setup(repo => repo.GrabAlbumById(testId)).Returns(new Album { Name = "Test" });

            // Act
            _albumService.FetchAlbumById(testId);

            // Assert
            _albumRepositoryMock.Verify(repo => repo.GrabAlbumById(testId), Times.Once);
        }

    }
}