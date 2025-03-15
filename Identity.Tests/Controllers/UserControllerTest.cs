using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zuhid.Identity.Controllers;
using Zuhid.Identity.Models;
using Zuhid.Identity.Mappers;

namespace Zuhid.Identity.Tests.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public async Task Get_without_guid_when_no_record_in_repo_returns_empty_array()
        {
            // Arrange
            var identityRepository = new Mock<IIdentityRepository>();
            identityRepository.Setup(repo => repo.Get()).ReturnsAsync([]);
            var userController = new UserController(identityRepository.Object, It.IsAny<IUserMapper>());

            // Act
            var actualResult = await userController.Get(null);

            // assert
            Assert.NotNull(actualResult);
            Assert.Empty(actualResult);
        }
    }
}