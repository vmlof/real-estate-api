namespace Homely.Application.Authentication;

public interface IUserContext
{
    int UserId { get; }

    string Email { get; }

    bool IsAuthenticated { get; }
}