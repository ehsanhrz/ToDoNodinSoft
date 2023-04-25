using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.UserAggregate.Events;
public class ChangePhoneVerficationCodeSenderEvent : DomainEventBase
{
  public ClientUser user { get; set; }

  public ChangePhoneVerficationCodeSenderEvent(ClientUser user)
  {
    this.user = user;
  }
}
