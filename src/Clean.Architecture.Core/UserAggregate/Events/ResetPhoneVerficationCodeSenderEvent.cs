using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.UserAggregate.Events;
public class ResetPhoneVerficationCodeSenderEvent : DomainEventBase
{
  public PhoneValidation phoneValidation { get; set; }

  public ResetPhoneVerficationCodeSenderEvent(PhoneValidation phoneValidation)
  {
    this.phoneValidation = phoneValidation;
  } 

}
