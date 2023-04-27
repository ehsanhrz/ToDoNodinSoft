using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Clean.Architecture.Core.UserAggregate.Events;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.SharedKernel.Interfaces;
using Ardalis.GuardClauses;

namespace Clean.Architecture.Core.UserAggregate.Handlers;
public class ResetPhoneVerficationCodeSenderHandler : INotificationHandler<ResetPhoneVerficationCodeSenderEvent>
{
  private ICodeSender _codeSender;
  private readonly IRepository<PhoneValidation> _repository;

  public ResetPhoneVerficationCodeSenderHandler(ICodeSender codeSender, IRepository<PhoneValidation> repository)
  {
    _codeSender = codeSender;
    _repository = repository;
  }
  public async Task Handle(ResetPhoneVerficationCodeSenderEvent notification, CancellationToken cancellationToken)
  {
    Guard.Against.Null(notification);
    var CodeGenerator = new Random();

    int Code = CodeGenerator.Next(100000, 999999);

    bool result = notification.phoneValidation.ResetCode(Code);

    if (result == true)
    {
      _repository.UpdateAsync(notification.phoneValidation, cancellationToken).Wait(cancellationToken);

      await _codeSender.SendCode(Code, notification.phoneValidation.UserPhoneNumber);
    }
  }
}
