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
    public class StockTransactionController : UserBaseController
    {
        private readonly IStockTransactionRepository _repo;
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public StockTransactionController(IStockTransactionRepository repo, IMapper mapper, UserManager<AppUser> userManager, IAppUserRepository userRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }


        [HttpPost]
        public async Task<IActionResult> List(ListRequestDto p)
        {
            var query = _repo.GetList().ToDynamicWhereAndOrder(p);

            var test = await PagerUtils<StockTransaction, StockTransactionDto>.SetAsync(query, _mapper, p.PageIndex, p.PageSize);
            return CreateActionResult(PagerResponseDto<StockTransactionDto>.Success(HttpStatusCode.OK, test.Items, test.ItemsInfo, 1));
        }


        [HttpPost]
        public async Task<IActionResult> Create(StockTransactionDto model)
        {
            var item = _mapper.Map<StockTransaction>(model);
            var result = await _repo.CreateAsync(item);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK, item.Id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(StockTransactionDto model)
        {
            var item = await _repo.GetItemAsync(model.Id);
            _mapper.Map(model, item);
            var result = await _repo.EditAsync(item);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK, item.Id));
        }


        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Item(int id)
        {
            var result = await _repo.GetItemAsync(id);
            var item = _mapper.Map<StockTransactionDto>(result);
            return CreateActionResult(ResponseDto<StockTransactionDto>.Success(HttpStatusCode.OK, item));
        }
    }
}