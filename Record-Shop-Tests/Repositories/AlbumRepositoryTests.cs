using Moq;
using Record_Shop_Backend.MVC_Repositories;
using Record_Shop_Backend.MVC_Services;

namespace Record_Shop_Tests.RepositoriesTests
{
    public class AlbumRepositoryTests
    {
        private AlbumRepository _albumRepository;

        [SetUp]
        public void Setup()
        {
            _albumRepository = new AlbumRepository();
        }

        [Test]
        public void Test1()
        {
            //Arrange


            //Act


            //Assert
            Assert.Pass();
        }
    }
}