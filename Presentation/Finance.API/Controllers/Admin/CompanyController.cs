using AutoMapper;
using Finance.Application.Dtos;
using Finance.Application.Repositories;
using Finance.Application.Utils;
using Finance.Application.Utils.Extensions;
using Finance.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Finance.API.Controllers
{
  
    public class CompanyController : AdminBaseController
    {
        private readonly ICompanyRepository _repo;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> List(ListRequestDto p)
        {
            var query = _repo.GetList().ToDynamicWhereAndOrder(p);

            var test = await PagerUtils<Company, CompanyDto>.SetAsync(query, _mapper, p.PageIndex, p.PageSize);
            return CreateActionResult(PagerResponseDto<CompanyDto>.Success(HttpStatusCode.OK, test.Items, test.ItemsInfo, 1));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyDto model)
        {
            var item = _mapper.Map<Company>(model);

            var result = await _repo.CreateAsync(item);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK, item.Id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyDto model)
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
            var item = _mapper.Map<CompanyDto>(result);
            return CreateActionResult(ResponseDto<CompanyDto>.Success(HttpStatusCode.OK, item));
        }
    }
}