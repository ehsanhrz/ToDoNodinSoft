using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.UserAggregate.Events;
using Clean.Architecture.Core.UserAggregate.Handlers;
using Clean.Architecture.Core.UserAggregate;
using Clean.Architecture.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.UserAggregate.Handlers;
public class ResetPhoneVerficationCodeSenderTest
{
  private ResetPhoneVerficationCodeSenderHandler _handler;
  private Mock<ICodeSender> _codeSenderMock;
  private Mock<IRepository<PhoneValidation>> _mockRepository;

  public ResetPhoneVerficationCodeSenderTest()
  {
    _codeSenderMock = new Mock<ICodeSender>();
    _mockRepository = new Mock<IRepository<PhoneValidation>>();
    _handler = new ResetPhoneVerficationCodeSenderHandler(_codeSenderMock.Object, _mockRepository.Object);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNullEventArgument()
  {
#nullable disable
    Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
#nullable enable
  }

  [Fact]
  public async Task SendsEmailGivenEventInstance()
  {
    await _handler.Handle(new ResetPhoneVerficationCodeSenderEvent(new PhoneValidation(new Guid(), 112233, "09141141598")), CancellationToken.None);

    _codeSenderMock.Verify(sender => sender.SendCode(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
  }

}
