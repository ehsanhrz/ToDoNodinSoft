using Clean.Architecture.Core.UserAggregate;
using Xunit;

namespace Clean.Architecture.IntegrationTests.Data;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
  [Fact]
  public async Task DeletesItemAfterAddingIt()
  {
    // add a project
    var testClientUserName = "testProject";

    var testClientPassword = "198874598";

    var repository = GetRepository();

    var User = new ClientUser(testClientUserName, testClientPassword);

    await repository.AddAsync(User);

    // delete the item
    await repository.DeleteAsync(User);

    // verify it's no longer there
    Assert.DoesNotContain(await repository.ListAsync(),
        project => project.UserName == testClientUserName);
  }
}
