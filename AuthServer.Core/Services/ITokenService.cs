using AuthServer.Core.Configurations;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;

namespace AuthServer.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
