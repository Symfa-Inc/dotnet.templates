namespace WebApiTemplate.Application.UserProfile.Interfaces
{
    public interface IUserContext
    {
        bool IsAuthorized();
        string UserId { get; }
    }
}
