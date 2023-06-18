using AutoMapper;
using Finance.Application.Dtos;
using Finance.Application.Repositories;
using Finance.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Finance.API.Controllers
{

    [Authorize(Roles = "admin")]
    public class UserController : AdminBaseController
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        public UserController(IAppUserRepository appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> List(ListRequestDtoById postModel)
        {
            var model = await _appUserRepository.GetUserList(postModel);
            return CreateActionResult(PagerResponseDto<UserDto>.Success(HttpStatusCode.OK, model.Items, model.ItemsInfo));
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserDto model)
        {
            var user = await _appUserRepository.CreateUser(model);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK, user.Id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserDto postModel)
        {
            _ = await _appUserRepository.EditUser(postModel);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK));
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Item(int id)
        {
            var result = await _appUserRepository.GetItemAsync(id);
            var item = _mapper.Map<UserDto>(result);
            return CreateActionResult(ResponseDto<UserDto>.Success(HttpStatusCode.OK, item));
        }
    }
}
