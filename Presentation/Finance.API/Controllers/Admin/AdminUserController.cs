using AutoMapper;
using Finance.Application.Dtos;
using Finance.Application.Repositories;
using Finance.Application.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Finance.API.Controllers
{
    public class AdminUserController : AdminBaseController
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        public AdminUserController(IAppUserRepository appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> List(ListRequestDto postModel)
        {
            var model = await _appUserRepository.GetAdminUserList(postModel);
            return CreateActionResult(PagerResponseDto<AdminListItemDto>.Success(HttpStatusCode.OK, model.Items, model.ItemsInfo));
        }


        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateDto model)
        {
            var user = await _appUserRepository.CreateAdminUser(model);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AdminEditDto postModel)
        {
            _ = await _appUserRepository.EditAdminUser(postModel);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK));
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Item(int id)
        {
            var result = await _appUserRepository.GetItemAsync(id);
            var item = _mapper.Map<AdminEditDto>(result);
            return CreateActionResult(ResponseDto<AdminEditDto>.Success(HttpStatusCode.OK, item));
        }
    }
}
