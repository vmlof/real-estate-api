namespace Homely.Application.Interfaces;

public interface ICurrentUser
{
    int UserId { get; }

    string Email { get; }

    bool IsAuthenticated { get; }
}