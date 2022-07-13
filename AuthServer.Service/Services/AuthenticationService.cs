using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Core.Configurations;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            UserApp user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null)  return ResponseDto<TokenDto>.Fail("Email or Password is wrong",400,true);

            if(!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return ResponseDto<TokenDto>.Fail("Email or Password is wrong", 400, true);
            }

            TokenDto token = _tokenService.CreateToken(user);

            UserRefreshToken userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if(userRefreshToken == null)
            {
                await _userRefreshTokenService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }


            await _unitOfWork.CommitAsync();
            return ResponseDto<TokenDto>.Success(token, 200);

        }

        public ResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            Client client = _client.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if(client == null)
            {
                return ResponseDto<ClientTokenDto>.Fail("ClientId or ClientSecret not found",404,true);
            }


            ClientTokenDto token = _tokenService.CreateTokenByClient(client);

            return ResponseDto<ClientTokenDto>.Success(token, 200);


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
