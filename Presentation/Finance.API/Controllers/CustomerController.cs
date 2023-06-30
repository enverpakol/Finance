using AutoMapper;
using Finance.Application.Dtos;
using Finance.Application.Repositories;
using Finance.Application.Utils;
using Finance.Application.Utils.Extensions;
using Finance.Domain.Entities;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Finance.API.Controllers
{
    public class CustomerController : UserBaseController
    {
        private readonly ICustomerRepository _repo;
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public CustomerController(ICustomerRepository repo, IMapper mapper, UserManager<AppUser> userManager, IAppUserRepository userRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListRequestDto p)
        {
            var list = await _repo.GetListFromCacheAsync();
            var filteredList = list.ToDynamicWhereAndOrder(p);

            var test = PagerUtils<Customer, CustomerDto>.SetAsync(filteredList, _mapper, p.PageIndex, p.PageSize);
            return CreateActionResult(PagerResponseDto<CustomerDto>.Success(HttpStatusCode.OK, test.Items, test.ItemsInfo, 1));
        }


        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto model)
        {
            var item = _mapper.Map<Customer>(model);
            var result = await _repo.CreateAsync(item);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK, item.Id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CustomerDto model)
        {
            var item = await _repo.GetItemAsync(model.Id);
            _mapper.Map(model, item);
            var result = await _repo.EditAsync(item);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK, item.Id));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Item(int id)
        {
            var result = await _repo.GetFromCacheAsync(id);
            var item = _mapper.Map<CustomerDto>(result);
            return CreateActionResult(ResponseDto<CustomerDto>.Success(HttpStatusCode.OK, item));
        }
    }
}