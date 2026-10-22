using System.Security.Claims;
using Homely.Application.Authentication;
using Homely.Application.UseCases.Login;
using Homely.Domain.Abstractions;
using Homely.Domain.Common;
using Homely.Domain.Entities;
using Homely.Domain.Errors;
using Homely.Domain.Repositories;
using MediatR;

namespace Homely.Application.UseCases.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<LoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenProvider _tokenProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtOptions _jwt;

    public RefreshTokenCommandHandler(
        IUserRepository userRepository,
        ITokenProvider tokenProvider,
        IRefreshTokenRepository refreshTokenRepository,
        JwtOptions jwt)
    {
        _userRepository = userRepository;
        _tokenProvider = tokenProvider;
        _refreshTokenRepository = refreshTokenRepository;
        _jwt = jwt;
    }

    public async Task<Result<LoginResponse>> Handle(RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var hash = RefreshTokenHasher.Hash(request.RefreshToken);
        var stored = await _refreshTokenRepository.GetByHashAsync(
            hash, cancellationToken);

        var now = DateTime.UtcNow;
        if (stored is null || stored.IsRevoked || stored.ExpiresAt <= now)
            return Result<LoginResponse>.Failure(RefreshTokenErrors.Invalid);

        var user = await _userRepository.GetByIdAsync(
            stored.UserId, cancellationToken);

        if (user is null)
            return Result<LoginResponse>.Failure(RefreshTokenErrors.Invalid);

        var accessToken = _tokenProvider.GenerateAccessToken(user);
        var rawReplacement = _tokenProvider.GenerateRefreshToken();

        var replacement = new RefreshTokens()
        {
            Id = Guid.NewGuid(),
            SessionId = stored.SessionId,
            UserId = user.Id,
            TokenHash = RefreshTokenHasher.Hash(rawReplacement),
            CreatedAt = now,
            ExpiresAt = now.AddDays(_jwt.RefreshTokenDays)
        };

        var rotated = await _refreshTokenRepository.RotateAsync(
            stored.Id, now, replacement, cancellationToken);

        if (!rotated)
            return Result<LoginResponse>.Failure(RefreshTokenErrors.Invalid);

        return Result<LoginResponse>.Success(
            new LoginResponse(accessToken, rawReplacement));
    }
}
