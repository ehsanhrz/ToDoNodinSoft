using Clean.Architecture.Core.UserAggregate;
using Xunit;

namespace Clean.Architecture.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsProjectAndSetsId()
  {
    // create the object

    var testClientUserName = "testProject";

    var testClientPassword = "198874598";

    var repository = GetRepository();

    var User = new ClientUser(testClientUserName, testClientPassword);

    await repository.AddAsync(User);

    var newUser = (await repository.ListAsync())
                    .FirstOrDefault();

    // compare the values with created ones

    Assert.Equal(testClientUserName, newUser?.UserName);
    Assert.Equal(testClientPassword, newUser?.Password);
    Assert.True(newUser?.Id != null);
  }
}
