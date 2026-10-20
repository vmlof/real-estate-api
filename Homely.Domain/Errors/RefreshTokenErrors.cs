using Homely.Domain.Common;

namespace Homely.Domain.Errors;

public static class RefreshTokenErrors
{
    public static readonly Error Invalid = new(
        "Auth.InvalidRefreshToken",
        "Invalid or expired refresh token.");
}