using Ardalis.Result;

namespace Clean.Architecture.Core.UserAggregate.Interfaces;
public interface IUserSignup
{
    public Task<Result> CheckUserNameUnique(string UserName);

    public Task<Result> CheckPhoneNumberUnique(string PhoneNumber);

    public Task<Result> FireVerficationCodeSenderEvent(ClientUser user);

    public Task<Result> ResetVerficationCodeSenderEvent(ClientUser user);

    public Task<Result> CheckPhoneNumberVerficationCode(ClientUser user, int code);

    public Task<Result<ClientUser>> CreateUser(string UserName, string PassWord, string PhoneNumber);

    public Task<Result> ChangeUserPhoneNumberForValidation(ClientUser user, string PhoneNumber);

    public Task<Result> CheckUniqueEmail(string Email);

    public Task<Result> FireEmailVerficationCodeSender(ClientUser user);

    public Task<Result> ResetEmailCodeVerficationsender(ClientUser user);

    public Task<Result> ChangeUserEmailForValidation(ClientUser user, string Email);

    public Task<Result> CeckEmailVerficationCode(ClientUser user, int code);

}
