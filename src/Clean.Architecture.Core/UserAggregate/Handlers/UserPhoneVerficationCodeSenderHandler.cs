using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.UserAggregate.Events;
using Clean.Architecture.SharedKernel.Interfaces;
using MediatR;

namespace Clean.Architecture.Core.UserAggregate.Handlers;
public class UserPhoneVerficationCodeSenderHandler : INotificationHandler<UserPhoneVerficationCodeSenderEvent>
{
  private ICodeSender _codeSender;
  private readonly IRepository<PhoneValidation> _repository;
  public UserPhoneVerficationCodeSenderHandler(ICodeSender codeSender, IRepository<PhoneValidation> repository)
  {
    _codeSender = codeSender;
    _repository = repository;
  }
  public async Task Handle(UserPhoneVerficationCodeSenderEvent notification, CancellationToken cancellationToken)
  {
    Guard.Against.Null(notification);

    var CodeGenerator = new Random();

    int Code = CodeGenerator.Next(100000, 999999);

    var PhoneValidation = new PhoneValidation(notification.user.Id, Code, notification.user.PhoneNumber);

    await _repository.AddAsync(PhoneValidation);

    await _codeSender.SendCode(Code, notification.user.PhoneNumber);

  }
}
