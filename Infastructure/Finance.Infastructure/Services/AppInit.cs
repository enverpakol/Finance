using Finance.Application.Abstractions;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;

namespace Finance.Infastructure.Services
{
    public class AppInit : IAppInit
    {
        private readonly AppData _context;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppInit(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppData context, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
        }

        public void Init()
        {
            //_context.Database.EnsureCreated();
#if (!DEBUG)

			if (_context.Database.GetPendingMigrations().Any())
				_context.Database.Migrate();
#endif

            if (!_roleManager.RoleExistsAsync("admin").Result)
            {
                var result = _roleManager.CreateAsync(new AppRole
                {
                    Name = "admin",
                    NormalizedName = "ADMIN",
                }).Result;
            }


            if (!_roleManager.RoleExistsAsync("user").Result)
            {
                var result = _roleManager.CreateAsync(new AppRole
                {
                    Name = "user",
                    NormalizedName = "USER",
                }).Result;
            }

            if (!_roleManager.RoleExistsAsync("client").Result)
            {
                var result = _roleManager.CreateAsync(new AppRole
                {
                    Name = "client",
                    NormalizedName = "CLIENT",
                }).Result;
            }

            var baseUser = _userManager.FindByNameAsync("epakol12@gmail.com").Result;
            if (baseUser == null)
            {
                var user = new AppUser()
                {
                    UserName = "epakol12@gmail.com",
                    Email = "epakol12@gmail.com",
                    FirstName = "Enver",
                    LastName = "Pakol",
                };
                var result = _userManager.CreateAsync(user, "123456").Result;

               

            }
            else
            {
                if (_userManager.IsInRoleAsync(baseUser, "admin").Result==false)
                {
                    var roleResult = _userManager.AddToRoleAsync(baseUser, "admin").Result;
                }
            }
        }
    }
}
