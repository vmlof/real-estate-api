using Homely.Application.Authentication;
using Homely.Domain.Abstractions;
using Homely.Domain.Common;
using Homely.Domain.Entities;
using Homely.Domain.Errors;
using Homely.Domain.Repositories;
using MediatR;

namespace Homely.Application.UseCases.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtOptions _jwt;

    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITokenProvider tokenProvider,
        IPasswordHasher passwordHasher,
        JwtOptions jwt)
    {
        _unitOfWork = unitOfWork;
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwt = jwt;
    }

    public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            return Result<LoginResponse>.Failure(LoginErrors.InvalidCredentials);
        }

        var now = DateTime.UtcNow;
        var rawRefreshToken = _tokenProvider.GenerateRefreshToken();

        await _refreshTokenRepository.AddAsync(
            new RefreshTokens()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TokenHash = RefreshTokenHasher.Hash(rawRefreshToken),
                CreatedAt = now,
                ExpiresAt = now.AddDays(_jwt.RefreshTokenDays)
            },
            cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<LoginResponse>.Success(
            new LoginResponse(_tokenProvider.GenerateAccessToken(user), rawRefreshToken));
    }
}