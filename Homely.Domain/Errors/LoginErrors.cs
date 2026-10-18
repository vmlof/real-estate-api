using Homely.Domain.Common;

namespace Homely.Domain.Errors;

public static class LoginErrors
{
    public static readonly Error InvalidCredentials = new Error(
        "Login.InvalidCredentials", "Invalid email or password");
}