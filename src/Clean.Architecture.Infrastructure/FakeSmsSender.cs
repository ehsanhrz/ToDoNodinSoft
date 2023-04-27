using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Interfaces;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace Clean.Architecture.Infrastructure;
public class FakeSmsSender : ICodeSender
{
  private readonly ILogger _logger;
  public FakeSmsSender(ILogger<FakeSmsSender> logger)
  {
    _logger = logger;
  }
  public Task SendCode(int code, string phoneNumber)
  {
    _logger.LogInformation("****************\n Not actually sending an sms to {phoneNumber} the Code {code} \n *********", phoneNumber, code);
    return Task.CompletedTask;
  }
}
