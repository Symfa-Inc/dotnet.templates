namespace WebApiTemplate.Application.UserProfile.Interfaces
{
    public interface IUserContext
    {
        bool IsAuthorized();
        int UserId { get; }
    }
}
