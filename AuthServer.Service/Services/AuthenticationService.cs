using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.Core.Configurations;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedLibrary.Dtos;

namespace AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _client;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _client = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            throw new System.NotImplementedException();
        }
        public Task<ResponseDto<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            throw new System.NotImplementedException();
        }
        public Task<ResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            throw new System.NotImplementedException();
        }
        public Task<ResponseDto<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
