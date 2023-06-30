using AutoMapper;
using Finance.Application.Dtos;
using Finance.Application.Exceptions;
using Finance.Application.Repositories;
using Finance.Application.Utils;
using Finance.Application.Utils.Extensions;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence.Repositories
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _accessor;
        public AppUserRepository(AppData context, UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor accessor) : base(context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task<AppUser> CreateAdminUser(AdminCreateDto model)
        {
            var user = _mapper.Map<AppUser>(model);

            var managerResult = await _userManager.CreateAsync(user, model.Password);
            if (!managerResult.Succeeded)
                throw new ClientSideException(string.Join(",", managerResult?.Errors.Select(x => x.Description)?.ToList() ?? new List<string>()));

            await _userManager.AddToRoleAsync(user, "admin");

            return user;
        }



        public async Task<PagerUtils<AppUser, AdminListItemDto>> GetAdminUserList(ListRequestDto p)
        {
            var adminIds = _userManager.GetUsersInRoleAsync("admin").GetOnlyIdList();
            var query = Context.AppUsers
                        .Where(x => adminIds.Contains(x.Id))
                          .ToDynamicOrder(p.OrderField, p.OrderDir)
                      .AsNoTracking();

            return await PagerUtils<AppUser, AdminListItemDto>.SetAsync(query, _mapper, p.PageIndex, p.PageSize);
        }

        public async Task<bool> EditAdminUser(AdminEditDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            if (user == null)
                throw new NotFoundException("");

            _mapper.Map(model, user);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new ClientSideException(string.Join(",", result?.Errors.Select(x => x.Description)?.ToList() ?? new List<string>()));

            if (model.Password != null)
            {
                var result2 = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result3 = await _userManager.ResetPasswordAsync(user, result2, model.Password);
            }


            return true;
        }

        public async Task<AppUser> CreateUser(UserDto model)
        {
            var user = _mapper.Map<AppUser>(model);

            var managerResult = await _userManager.CreateAsync(user, model.Password);
            if (!managerResult.Succeeded)
                throw new ClientSideException(string.Join(",", managerResult?.Errors.Select(x => x.Description)?.ToList() ?? new List<string>()));

            await _userManager.AddToRoleAsync(user, "user");


            return user;
        }

        public async Task<bool> EditUser(UserDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            if (user == null)
                throw new NotFoundException("");

            _mapper.Map(model, user);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new ClientSideException(string.Join(",", result?.Errors.Select(x => x.Description)?.ToList() ?? new List<string>()));

            if (model.Password != null)
            {
                var result2 = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result3 = await _userManager.ResetPasswordAsync(user, result2, model.Password);
            }


            return true;
        }

        public async Task<PagerUtils<AppUser, UserDto>> GetUserList(ListRequestDtoById model)
        {
            var adminIds = _userManager.GetUsersInRoleAsync("user").GetOnlyIdList();
            var query = Context.AppUsers
                        .Where(x => adminIds.Contains(x.Id) && x.CompanyId == model.Id)
                        .AsNoTracking();

            return await PagerUtils<AppUser, UserDto>.SetAsync(query, _mapper, model.PageIndex, model.PageSize);
        }

        public async Task<AppUser> GetActiveUser()
        {
            var user = await Table.FirstOrDefaultAsync(x => x.UserName == _accessor.HttpContext.User.Identity.Name);
            if (user == null)
                new NotFoundException("Aktif user bilgisi bulunamadı!");
            return user;
        }



    }
}
