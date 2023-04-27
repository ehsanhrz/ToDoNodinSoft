using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.UserAggregate.Events;
using Clean.Architecture.SharedKernel;
using Clean.Architecture.SharedKernel.Interfaces;
using MediatR;

namespace Clean.Architecture.Core.UserAggregate.Handlers;
public class ChangePhoneVerficationCodeSenderHandler : INotificationHandler<ChangePhoneVerficationCodeSenderEvent>
{
  private ICodeSender _codeSender;
  private readonly IRepository<PhoneValidation> _repository;
  public ChangePhoneVerficationCodeSenderHandler(ICodeSender codeSender, IRepository<PhoneValidation> repository)
  {
    _codeSender = codeSender;
    _repository = repository;
  }
  public async Task Handle(ChangePhoneVerficationCodeSenderEvent notification, CancellationToken cancellationToken)
  {
    Guard.Against.Null(notification);
    var CodeGenerator = new Random();

    int Code = CodeGenerator.Next(100000, 999999);

    var PhoneValidation = new PhoneValidation(notification.user.Id, Code, notification.user.PhoneNumber);

    _repository.AddAsync(PhoneValidation).Wait(cancellationToken);

    await _codeSender.SendCode(Code, notification.user.PhoneNumber);
  }
}
