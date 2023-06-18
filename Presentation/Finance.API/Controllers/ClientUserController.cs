using AutoMapper;
using Finance.Application.Dtos;
using Finance.Application.Repositories;
using Finance.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Finance.API.Controllers
{

    public class ClientUserController : UserBaseController
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        public ClientUserController(IAppUserRepository appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> List(ListRequestDto postModel)
        {
            var model = await _appUserRepository.GetClientUserList(postModel);
            return CreateActionResult(PagerResponseDto<ClientDto>.Success(HttpStatusCode.OK, model.Items, model.ItemsInfo));
        }


        [HttpPost]
        public async Task<IActionResult> Create(ClientDto model)
        {
            var user = await _appUserRepository.CreateClientUser(model);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK, user.Id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ClientDto postModel)
        {
            _ = await _appUserRepository.EditClientUser(postModel);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK));
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Item(int id)
        {
            var result = await _appUserRepository.GetItemAsync(id);
            var item = _mapper.Map<ClientDto>(result);
            return CreateActionResult(ResponseDto<ClientDto>.Success(HttpStatusCode.OK, item));
        }
    }
}
