using Clean.Architecture.Core.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Clean.Architecture.IntegrationTests.Data;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
  [Fact]
  public async Task UpdatesItemAfterAddingIt()
  {
    // add a project

    var testClientUserName = "testProject";

    var testClientPassword = "198874598";

    var repository = GetRepository();

    var User = new ClientUser(testClientUserName, testClientPassword);

    await repository.AddAsync(User);

    // detach the item so we get a different instance
    _dbContext.Entry(User).State = EntityState.Detached;

    // fetch the item and update its title
    var newProject = (await repository.ListAsync())
        .FirstOrDefault(project => project.UserName == testClientUserName);
    if (newProject == null)
    {
      Assert.NotNull(newProject);
      return;
    }
    
    var newName = Guid.NewGuid().ToString();
    
    newProject.Name = newName;

    // Update the item
    await repository.UpdateAsync(newProject);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
        .FirstOrDefault(project => project.Name == newName);

    Assert.NotNull(updatedItem);
    Assert.NotEqual(User.Name, updatedItem?.Name);
    Assert.Equal(newProject.UserName, updatedItem?.UserName);
    Assert.Equal(newProject.Id, updatedItem?.Id);
    Assert.Equal(newProject.Password, updatedItem?.Password);
  }
}
