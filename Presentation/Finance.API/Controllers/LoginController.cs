using AutoMapper;
using ClosedXML.Excel;
using Finance.Application.Abstractions;
using Finance.Application.Dtos;
using Finance.Application.Exceptions;
using Finance.Application.Utils;
using Finance.Application.Utils.Extensions;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Net;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Finance.API.Controllers
{
    public class LoginController : CustomBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;
        private readonly AppData _clientContext;
        public LoginController(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, AppData data)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _clientContext = data;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                throw new ClientSideException("Kullanıcı adı veya şifre hatalı !");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (!result.Succeeded)
                throw new ClientSideException("Kullanıcı adı veya şifre hatalı !");



            var tokenInfo = _tokenHandler.CreateAccessToken(100, user.Id);
            tokenInfo.FirstName = user.FirstName;
            tokenInfo.Email = user.Email;
            tokenInfo.LastName = user.LastName;


            return CreateActionResult(ResponseDto<Token>.Success(HttpStatusCode.OK, tokenInfo));
        }


    }
}
