using Homely.Application.Authentication;
using Homely.Domain.Abstractions;
using Homely.Domain.Repositories;
using MediatR;

namespace Homely.Application.UseCases.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, ITokenProvider tokenProvider,
        IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _unitOfWork = unitOfWork;
        _tokenProvider = tokenProvider;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<LoginResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await _refreshTokenRepository.GetRefreshToken(request.RefreshToken);

        if (storedToken is null)
        {
            return null;
        }

        storedToken.IsRevoked = true;

        var user = await _userRepository.GetByIdAsync(storedToken.UserId);

        var (accessToken, newRefreshToken) = _tokenProvider.GenerateTokens(user!);

        await _unitOfWork.CommitAsync();


        return new LoginResponse(accessToken, newRefreshToken);
    }
}