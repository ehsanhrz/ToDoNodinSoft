using Ardalis.Result;
using Clean.Architecture.Core.UserAggregate.Events;
using Clean.Architecture.Core.UserAggregate.Interfaces;
using Clean.Architecture.Core.UserAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;
using MediatR;

namespace Clean.Architecture.Core.UserAggregate.Services;
public class UserSignupService : IUserSignup
{

    private readonly IRepository<ClientUser> _clientUserRepository;
    private readonly IRepository<PhoneValidation> _phoneValidationRepository;
    private readonly IMediator _mediator;
    public UserSignupService(IRepository<ClientUser> repositoryClientUser, IRepository<PhoneValidation> repositoryPhoneValidation, IMediator mediator)
    {
        _clientUserRepository = repositoryClientUser;
        _phoneValidationRepository = repositoryPhoneValidation;
        _mediator = mediator;
    }

    public Task<Result> CeckEmailVerficationCode(ClientUser user, int code)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ChangeUserEmailForValidation(ClientUser user, string Email)
    {
        throw new NotImplementedException();
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
                user.VerifiUser();

                await _clientUserRepository.UpdateAsync(user);

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

    public Task<Result> CheckUniqueEmail(string Email)
    {
        throw new NotImplementedException();
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

    public async Task<Result<ClientUser>> CreateUser(string UserName, string PassWord, string PhoneNumber)
    {
        try
        {
            var newUser = new ClientUser(UserName, PassWord, PhoneNumber);

            await _clientUserRepository.AddAsync(newUser);

            await _clientUserRepository.SaveChangesAsync(new CancellationToken());

            return Result.Success(newUser);
        }
        catch
        {
            return Result.Error("Enternal DataBase Error");
        }


    }

    public Task<Result> FireEmailVerficationCodeSender(ClientUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> FireVerficationCodeSenderEvent(ClientUser user)
    {
        var CodeSenderEvent = new UserPhoneVerficationCodeSenderEvent(user);

        await _mediator.Publish(CodeSenderEvent);

        return Result.Success();
    }

    public Task<Result> ResetEmailCodeVerficationsender(ClientUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> ResetVerficationCodeSenderEvent(ClientUser user)
    {
        var FindePhoneValidationSpec = new FindePhoneValidationByUser(user.Id, user.PhoneNumber);

        var searchResult = await _phoneValidationRepository.FirstOrDefaultAsync(FindePhoneValidationSpec);

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
