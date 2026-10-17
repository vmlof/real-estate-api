using Homely.Application.Authentication;
using Homely.Domain.Abstractions;
using Homely.Domain.Repositories;
using MediatR;

namespace Homely.Application.UseCases.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, ITokenProvider tokenProvider,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            return null;

        var (accessToken, refreshToken) = _tokenProvider.GenerateTokens(user);

        await _unitOfWork.CommitAsync();

        return new LoginResponse(accessToken, refreshToken);
    }
}