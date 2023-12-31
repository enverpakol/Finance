using AutoMapper;
using Finance.Application.Dtos;
using Finance.Application.Dtos.FilterDtos;
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
    public class InvoiceController : UserBaseController
    {
        private readonly IInvoiceRepository _repo;
        private readonly IMapper _mapper;
        public InvoiceController(IInvoiceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> List([FromQuery] InvoiceFilterDto p)
        {
            var query = _repo.GetList(x=>
            (p.CustomerId == null || x.CustomerId==p.CustomerId)
            &&(p.InvoiceNo == null || x.No.Contains(p.InvoiceNo))
            && x.CreatedDate.Date>=p.BeginDate.Date
            && x.CreatedDate.Date<=p.EndDate.Date
            ).ToDynamicOrder(p.OrderField,p.OrderDir);

            var test = await PagerUtils<Invoice, InvoiceDto>.SetAsync(query, _mapper, p.PageIndex, p.PageSize);
            return CreateActionResult(PagerResponseDto<InvoiceDto>.Success(HttpStatusCode.OK, test.Items, test.ItemsInfo, 1));
        }


        [HttpPost]
        public async Task<IActionResult> Create(InvoiceDto model)
        {
            var item = _mapper.Map<Invoice>(model);
            var result = await _repo.CreateAsync(item);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK, item.Id));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(InvoiceDto model)
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
            var result = await _repo.GetItemAsync(id);
            var item = _mapper.Map<InvoiceDto>(result);
            return CreateActionResult(ResponseDto<InvoiceDto>.Success(HttpStatusCode.OK, item));
        }
    }
}
