// using Microsoft.Data.Sqlite;
// using Microsoft.EntityFrameworkCore;
// using Zuhid.Identity.Controllers;
// using Zuhid.Identity.Mappers;
// using Zuhid.Identity.Models;
// using Zuhid.Identity;

// namespace Zuhid.Identity.Tests.Controllers;

// public class UserControllerTest
// {
//     [Fact]
//     public async Task Crud_Test()
//     {
//         // Arrange
//         var connection = new SqliteConnection("Filename=:memory:");
//         connection.Open();
//         var dbContext = new DbContextOptionsBuilder<IdentityContext>()
//             .LogTo(Console.WriteLine)
//             .EnableSensitiveDataLogging()
//             .UseSqlite(connection);
//         var identityContext = new IdentityContext(dbContext.Options);
//         identityContext.Database.EnsureDeleted();
//         identityContext.Database.EnsureCreated();
//         var identityRepository = new IdentityRepository(identityContext);
//         var userController = new UserController(identityRepository, new UserMapper());

//         // Get
//         var actualResult = await userController.Get(null);
//         // assert
//         Assert.NotNull(actualResult);
//         Assert.Empty(actualResult);

//         // add
//         var saveResponse = await userController.Add(new User
//         {
//             Id = new Guid("f08b13f0-1e24-48f7-a205-029de54eedc6"),
//             UpdatedDate = new DateTime(2025, 03, 14),
//             UpdatedBy = "Administrator",
//             IsActive = true,
//             Email = "test@test.com",
//             Password = "P@ssw0rd",
//             Phone = "333-333-3333"
//         });
//         Assert.NotNull(saveResponse);

//         // Get with Id
//         actualResult = await userController.Get(new Guid("f08b13f0-1e24-48f7-a205-029de54eedc6"));
//         Assert.NotEmpty(actualResult);

//         // update
//         identityContext.ChangeTracker.Clear();
//         saveResponse = await userController.Update(new User
//         {
//             Id = new Guid("f08b13f0-1e24-48f7-a205-029de54eedc6"),
//             UpdatedDate = saveResponse.Updated,
//             UpdatedBy = "Administrator",
//             IsActive = true,
//             Email = "test@test.com",
//             Password = "P@ssw0rd",
//             Phone = "333-333-3333"

//         });
//         Assert.NotNull(saveResponse);


//         // delete
//         await userController.Delete(new Guid("f08b13f0-1e24-48f7-a205-029de54eedc6"));
//         Assert.NotNull(saveResponse);
//     }
// }