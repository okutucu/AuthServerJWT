using System.Threading.Tasks;
using AuthServer.Core.DTOs;
using SharedLibrary.Dtos;

namespace AuthServer.Core.Services
{
    public interface IUserAppService 
    {
        Task<ResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<ResponseDto<UserAppDto>> GetUserByNameAsync(string userName);

    }
}
