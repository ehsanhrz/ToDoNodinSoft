using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.UserAggregate;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.UserAggregate;
public class PhoneValidationConstructor
{

  private readonly Guid _testId = new Guid();
  private readonly int _testCode = 656565;
  private readonly string _testPhoneNumber = "09141178787";
  private PhoneValidation? _testPhoneValidation;

  private PhoneValidation CreatePhoneValidation()
  {
    return new PhoneValidation(_testId, _testCode, _testPhoneNumber);
  }

  [Fact]
  public void InitializesPhoneValidation()
  {
    _testPhoneValidation = CreatePhoneValidation();

    Assert.Equal(_testId, _testPhoneValidation.Id);
    Assert.Equal(_testCode, _testPhoneValidation.Code);
    Assert.Equal(_testPhoneNumber, _testPhoneValidation.UserPhoneNumber);
  }
}
