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
    public class StockController : UserBaseController
    {
        private readonly IStockRepository _repo;
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public StockController(IStockRepository repo, IMapper mapper, UserManager<AppUser> userManager, IAppUserRepository userRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<IActionResult> List([FromQuery]ListRequestDto p)
        {
            var list = await _repo.GetListFromCacheAsync();
            var filteredList = list.ToDynamicWhereAndOrder(p);

            var test = PagerUtils<Stock, StockDto>.SetAsync(filteredList, _mapper, p.PageIndex, p.PageSize);
            return CreateActionResult(PagerResponseDto<StockDto>.Success(HttpStatusCode.OK, test.Items, test.ItemsInfo, 1));
        }


        [HttpPost]
        public async Task<IActionResult> Create(StockDto model)
        {
            var item = _mapper.Map<Stock>(model);
            var result = await _repo.CreateAsync(item);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK, item.Id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(StockDto model)
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
            var item = _mapper.Map<StockDto>(result);
            return CreateActionResult(ResponseDto<StockDto>.Success(HttpStatusCode.OK, item));
        }
    }
}