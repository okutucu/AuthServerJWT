using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Dtos;

namespace AuthServer.Service.Services
{
    public class UserService : IUserAppService
    {
        private readonly UserManager<UserApp> _userManager;

        public UserService(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            UserApp user = new UserApp { Email = createUserDto.Email,UserName = createUserDto.UserName};

            IdentityResult result = await _userManager.CreateAsync(user, createUserDto.Password);

            if(!result.Succeeded)
            {
                List<string> errors = result.Errors.Select(x => x.Description).ToList();

                return ResponseDto<UserAppDto>.Fail(new ErrorDto(errors,true),400);
            }

            return ResponseDto<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);

        }

        public async Task<ResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            UserApp user = await _userManager.FindByNameAsync(userName);

            if(user == null)
            {
                return ResponseDto<UserAppDto>.Fail("Username not found",404,true);
            }

            return ResponseDto<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }
    }
}
