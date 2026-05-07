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
                Artist = "Eden",
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
                Artist = "Eden",
                ReleaseYear = "2015",
                Genre = "Electronic / Alt-Pop",
                Price = 12.99,
                Stock = 25
            };
            Album ityttmom = new Album
            {
                AlbumId = 0,
                Name = "i think you think too much of me",
                Artist = "Eden",
                ReleaseYear = "2016",
                Genre = "Indie Pop / Rock",
                Price = 14.99,
                Stock = 15
            };
            Album dark = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                Artist = "Eden",
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


        [Test]
        public void GrabAlbumById_ReturnsNull_WhenDbEmpty()
        {
            //Arrange
            var id = 2;

            //Act
            var result = _albumRepository.GrabAlbumById(id);

            //Assert
            Assert.That(result, Is.Null);
        }
        [Test]
        public void GrabAlbumById_ReturnsNull_WhenInput0()
        {
            //Arrange
            var id = 0;

            //Act
            var result = _albumRepository.GrabAlbumById(id);

            //Assert
            Assert.That(result, Is.Null);
        }


        [Test]
        public void GrabAlbumById_ReturnsAlbum_WhenIdMatchesAlbum()
        {
            //Arrange
            var id = 1;
            Album dark = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            _context.Albums.Add(dark);
            _context.SaveChanges();

            //Act
            var result = _albumRepository.GrabAlbumById(id);

            //Assert
            Assert.That(result, Is.EqualTo(dark));
        }
        [Test]
        public void GrabAlbumById_ReturnsAlbum_WhenMultipleAlbums()
        {
            //Arrange
            var id = 1;
            var idTwo = 2;
            Album ityttmom = new Album
            {
                AlbumId = 0,
                Name = "i think you think too much of me",
                Artist = "Eden",
                ReleaseYear = "2016",
                Genre = "Indie Pop / Rock",
                Price = 14.99,
                Stock = 15
            };
            Album dark = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            _context.Albums.Add(dark);
            _context.Albums.Add(ityttmom);
            _context.SaveChanges();

            //Act
            var result = _albumRepository.GrabAlbumById(id);
            var result2 = _albumRepository.GrabAlbumById(idTwo);

            //Assert
            Assert.That(result, Is.EqualTo(dark));
            Assert.That(result2, Is.EqualTo(ityttmom));
        }


        [Test]
        public void SubmitAlbum_ReturnsAlbum_IfValid()
        {
            //Arrange
            Album album = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };

            //Act
            var result = _albumRepository.SubmitAlbum(album);

            //Assert
            Assert.That(result, Is.EqualTo(album));
        }
        [Test]
        public void SubmitAlbum_ReturnsAlbum_IfDatabaseUpdated()
        {
            //Arrange
            Album album = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var albums = new List<Album> { album };

            //Act
            var result = _albumRepository.SubmitAlbum(album);

            //Assert
            Assert.That(_context.Albums, Is.EquivalentTo(albums));
        }
        [Test]
        public void SubmitAlbum_ReturnsAlbum_IfIdInvalid()
        {
            //Arrange
            Album album = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var albums = new List<Album> { album };

            //Act
            var result = _albumRepository.SubmitAlbum(album);

            //Assert
            Assert.That(_context.Albums, Is.EquivalentTo(albums));
        }
        [Test]
        public void SubmitAlbum_ReturnsNull_IfAlbumAlreadyExists()
        {
            //Arrange
            Album album = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            _context.Albums.Add(album);
            _context.SaveChanges();

            //Act
            var result = _albumRepository.SubmitAlbum(album);


            //Assert
            Assert.That(result, Is.Null);
        }


        [Test]
        public void AlterAlbum_ReturnsAlbum_IfValid()
        {
            //Arrange
            Album album = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };

            //Act
            var result = _albumRepository.AlterAlbum(album);


            //Assert
            Assert.That(result, Is.EqualTo(album));
        }
        [Test]
        public void AlterAlbum_ReturnsAlbum_IfDatabaseUpdated()
        {
            //Arrange
            Album album = new Album
            {
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var albums = new List<Album> { album };

            //Act
            var result = _albumRepository.AlterAlbum(album);

            //Assert
            Assert.That(_context.Albums, Is.EquivalentTo(albums));
            Assert.That(result.Name == album.Name);

        }
        [Test]
        public void AlterAlbum_ReturnsAlbum_IfDatabaseCreatesNew()
        {
            //Arrange
            Album album = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var albums = new List<Album> { album };

            //Act
            var result = _albumRepository.AlterAlbum(album);

            //Assert
            Assert.That(_context.Albums, Is.EquivalentTo(albums));
            Assert.That(result.Name == "this album has been created");
        }


        [Test]
        public void DestroyAlbum_ReturnsAlbum_IfValidAndDeleted()
        {
            //Arrange
            Album album = new Album
            {
                AlbumId = 0,
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };

            //Act
            var result = _albumRepository.AlterAlbum(album);


            //Assert
            Assert.That(result, Is.EqualTo(album));
        }
        [Test]
        public void DestroyAlbum_ReturnsNewName_IfIdNotExists()
        {
            //Arrange
            Album album = new Album
            {
                Name = "Dark",
                Artist = "Eden",
                ReleaseYear = "2025",
                Genre = "Glitch Hop / Alternative R&B",
                Price = 18.99,
                Stock = 50
            };
            var newAlbum = album;
            newAlbum.Name = "this album doesn't exist";
            var albums = new List<Album> { album };

            //Act
            var result = _albumRepository.AlterAlbum(album);

            //Assert
            Assert.That(_context.Albums, Is.EquivalentTo(albums));
            Assert.That(result.Name == album.Name);
        }
    }
}