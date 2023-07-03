using Finance.Application.Abstractions;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Configuration;

namespace Finance.Infastructure.Services
{
    public class AppInit : IAppInit
    {
        private readonly AppData _context;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDistributedCache _cache;


        public AppInit(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppData context, SignInManager<AppUser> signInManager, IConnectionMultiplexer redisConnection, IDistributedCache cache)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _redisConnection = redisConnection;
            _cache = cache;
        }

        public async Task InitAsync()
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

            var baseUser = _userManager.FindByNameAsync("admin@test.com").Result;
            if (baseUser == null)
            {
                var user = new AppUser()
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    FirstName = "Admin",
                    LastName = "Test",
                };
                var result = _userManager.CreateAsync(user, "123456").Result;
                var roleResult = _userManager.AddToRoleAsync(user, "admin").Result;
            }
            else
            {
                if (_userManager.IsInRoleAsync(baseUser, "admin").Result == false)
                {
                    var roleResult = _userManager.AddToRoleAsync(baseUser, "admin").Result;
                }
            }


            var endpoints = _redisConnection.GetEndPoints();
            var server = _redisConnection.GetServer(endpoints.First());
            var test =  server.Keys().ToList();

            foreach (var item in test)
            {
                 await _cache.RemoveAsync(item.ToString());
            }
        }
    }
}
