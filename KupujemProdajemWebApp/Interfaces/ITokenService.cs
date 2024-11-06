using KupujemProdajemWebApp.Models;

namespace KupujemProdajemWebApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
