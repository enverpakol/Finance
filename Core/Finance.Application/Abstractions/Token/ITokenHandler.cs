using Finance.Application.Dtos;

namespace Finance.Application.Abstractions
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute, int userId);
    }
}
