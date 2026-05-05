using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Record_Shop_Backend.Data;
using Record_Shop_Backend.MVC_Data_Models;
using Record_Shop_Backend.MVC_Repositories;
using Record_Shop_Backend.MVC_Services;

namespace Record_Shop_Tests.RepositoriesTests
{
    public class AlbumRepositoryTests
    {
        private AlbumRepository _albumRepository;
        private AlbumDbContext _context;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<AlbumDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new AlbumDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _albumRepository = new AlbumRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Dispose();
        }

        [Test]
        public void GrabAllAlbums_ReturnsEmpty_WhenDbEmpty()
        {
            //Arrange


            //Act
            var result = _albumRepository.GrabAllAlbums();

            //Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GrabAllAlbums_ReturnsSingle_WhenDbSingle()
        {
            //Arrange
            Album endCredits = new Album
            {
                AlbumId = 0,
                Name = "End Credits",
                ReleaseYear = "2015",
                Genre = "Electronic / Alt-Pop",
                Price = 12.99,
                Stock = 25
            };
            _context.Albums.Add(endCredits);
            _context.SaveChanges();
            //Act
            var result = _albumRepository.GrabAllAlbums();

            //Assert
            var expected = new List<Album> { endCredits };
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void GrabAllAlbums_ReturnsMultiple_WhenDbMultiple()
        {
            //Arrange
            Album endCredits = new Album
            {
                AlbumId = 0,
                Name = "End Credits",
                ReleaseYear = "2015",
                Genre = "Electronic / Alt-Pop",
                Price = 12.99,
                Stock = 25
            };
            Album ityttmom = new Album
            {
                AlbumId = 0,
                Name = "i think you think too much of me",
                ReleaseYear = "2016",
                Genre = "Indie Pop / Rock",
                Price = 14.99,
                Stock = 15
            };
            Album dark = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var albumsToAdd = new List<Album> { endCredits, ityttmom, dark};
            _context.Albums.AddRange(albumsToAdd);
            _context.SaveChanges();
            //Act
            var result = _albumRepository.GrabAllAlbums();

            //Assert
            Assert.That(result, Is.EquivalentTo(albumsToAdd));
        }
    }
}