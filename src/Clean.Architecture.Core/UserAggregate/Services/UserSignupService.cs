﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Clean.Architecture.Core.UserAggregate.Events;
using Clean.Architecture.Core.UserAggregate.Interfaces;
using Clean.Architecture.Core.UserAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;
using MediatR;

namespace Clean.Architecture.Core.UserAggregate.Services;
public class UserSignupService : IUserSignup
{

  private IRepository<ClientUser> _clientUserRepository;
  private IRepository<PhoneValidation> _phoneValidationRepository;
  private IMediator _mediator;
  public UserSignupService(IRepository<ClientUser> RepositoryClientUser, IRepository<PhoneValidation> RepositoryPhoneValidation, IMediator mediator)
  {
    _clientUserRepository = RepositoryClientUser;
    _phoneValidationRepository = RepositoryPhoneValidation;
    _mediator = mediator;
  }

  public async Task<Result> ChangeUserPhoneNumberForValidation(ClientUser user, string PhoneNumber)
  {
    user.PhoneNumber = PhoneNumber;

    var ChangePhoneEvent = new ChangePhoneVerficationCodeSenderEvent(user);

    await _mediator.Publish(ChangePhoneEvent);

    return Result.Success();
  }

  public async Task<Result> CheckPhoneNumberUnique(string PhoneNumber)
  {
    var CheckPhoneNumberSpec = new CheckPhoneNumberUnique(PhoneNumber);

    var searchResult = await _clientUserRepository.FirstOrDefaultAsync(CheckPhoneNumberSpec);

    if (searchResult != null)
    {
      return Result.Error("this phone number alredy exists");
    }
    return Result.Success();
  }

  public async Task<Result> CheckPhoneNumberVerficationCode(ClientUser user, int code)
  {
    var CheckCodeSpec = new CheckPhoneNumberVerficationCode(user.PhoneNumber, user.Id, code);

    var SearchResult = await _phoneValidationRepository.FirstOrDefaultAsync(CheckCodeSpec);

    if (SearchResult != null)
    {
      if (SearchResult.ValidTime > DateTime.UtcNow)
      {
        return Result.Success();
      }
      else
      {
        return Result.Error("the code has exipred");
      }
    }
    else
    {
      return Result.Error("verfication code is not valid");
    }

  }

  public async Task<Result> CheckUserNameUnique(string UserName)
  {
    var UserNameUniqueSpec = new CheckUserNameUnique(UserName);

    var SearchResult = await _clientUserRepository.FirstOrDefaultAsync(UserNameUniqueSpec);

    if (SearchResult != null)
    {
      return Result.Error("the username is AlredyExists");
    }
    return Result.Success();
  }

  public async Task<Result<ClientUser>> CreateUser(string UserName, string PassWord)
  {
    try
    {
      var newUser = new ClientUser(UserName, PassWord);

      await _clientUserRepository.AddAsync(newUser);

      await _clientUserRepository.SaveChangesAsync(new CancellationToken());

      return Result.Success(newUser);
    }
    catch
    {
      return Result.Error("Enternal DataBase Error");
    }


  }


  public async Task<Result> FireVerficationCodeSenderEvent(ClientUser user)
  {
    var CodeSenderEvent = new UserPhoneVerficationCodeSenderEvent(user);

    await _mediator.Publish(CodeSenderEvent);

    return Result.Success();
  }

  public async Task<Result> ResetVerficationCodeSenderEvent(ClientUser user)
  {
    var FindePhoneValidationSpec = new FindePhoneValidationByUser(user.Id, user.PhoneNumber);

    var searchResult =  await _phoneValidationRepository.FirstOrDefaultAsync(FindePhoneValidationSpec);

    try
    {
      if (searchResult != null)
      {

        var ResetCodeEvent = new ResetPhoneVerficationCodeSenderEvent(searchResult);

        await _mediator.Publish(ResetCodeEvent);

        return Result.Success();
      }
      else
      {
        var sendCode = new UserPhoneVerficationCodeSenderEvent(user);

        await _mediator.Publish(sendCode);

        return Result.Success();
      }
    }
    catch { return Result.Error("internal Server Error"); }
    

  }

}
