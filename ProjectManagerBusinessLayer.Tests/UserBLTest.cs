
using ProjectManagerEntities;
using System.Linq;
using Xunit;

namespace ProjectManagerBusinessLayer.Tests
{
    public class UserBLTest : IClassFixture<DbContextFixture>
    {
        private readonly UserBL userBL;
        public DbContextFixture _dbfixture;
        public UserBLTest(DbContextFixture dbFixture)
        {
            _dbfixture = dbFixture;
            userBL = new UserBL(_dbfixture.dbContext);
        }

        [Fact]
        public void GetAll()
        {
            var users = userBL.RetrieveAllData();

            Assert.NotNull(users);
        }

        [Fact]
        public void SearchUsers()
        {
            var users = userBL.SearchByKey("Test user");

            Assert.NotNull(users);
        }

        [Fact]
        public void Add()
        {
            var id = userBL.CreateNew(new User
            {
                FirstName = "Test user2",
                EmployeeId = "1234"
            });

            Assert.True(id > 0);
        }

        [Fact]
        public void Update()
        {
            var id = userBL.CreateNew(new User
            {
                FirstName = "Test user2",
                EmployeeId = "12345"
            });

            var newName = "Test user update";
            var user = userBL.RetrieveAllData().First(m => m.UserId == id);
            user.FirstName = newName;

            user = userBL.Update(user);

            Assert.True(user.FirstName == newName);
        }

        [Fact]
        public void Delete()
        {
            userBL.Delete(1);

            var user = userBL.RetrieveAllData().FirstOrDefault(m => m.UserId == 1);

            Assert.Null(user);
        }
    }
}
