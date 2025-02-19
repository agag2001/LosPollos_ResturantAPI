namespace LosPollos.Application.User
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}