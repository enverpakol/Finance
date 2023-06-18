using Finance.Application.Dtos;
using Finance.Application.Utils;
using Finance.Domain.Entities.Identity;

namespace Finance.Application.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetActiveUser();
        Task<AppUser> CreateAdminUser(AdminCreateDto model);
        Task<bool> EditAdminUser(AdminEditDto model);
        Task<PagerUtils<AppUser, AdminListItemDto>> GetAdminUserList(ListRequestDto model);


        Task<AppUser> CreateUser(UserDto model);
        Task<bool> EditUser(UserDto model);
        Task<PagerUtils<AppUser, UserDto>> GetUserList(ListRequestDtoById model);



        Task<AppUser> CreateClientUser(ClientDto model);
        Task<bool> EditClientUser(ClientDto model);
        Task<PagerUtils<AppUser, ClientDto>> GetClientUserList(ListRequestDto model);


    }
}
