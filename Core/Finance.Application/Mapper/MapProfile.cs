using AutoMapper;
using Finance.Application.Dtos;
using Finance.Application.Utils;
using Finance.Domain.Entities;
using Finance.Domain.Entities.Identity;

namespace Finance.Application.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AdminEditDto, AppUser>()
                /*.ForMember(x => x.Email, g => g.MapFrom(x => x.UserName))*/;

            CreateMap<AppUser, AdminEditDto>();


            CreateMap<AdminCreateDto, AppUser>()
                .ForMember(x => x.Email, g => g.MapFrom(x => x.UserName));
            CreateMap<AdminListItemDto, AppUser>().ReverseMap();


            CreateMap<AppUser, UserDto>();

            CreateMap<UserDto, AppUser>()
                .ForMember(x => x.Email, g => g.MapFrom(x => x.UserName));



            CreateMap<AppUser, ClientDto>();

            CreateMap<ClientDto, AppUser>()
                .ForMember(x => x.Email, g => g.MapFrom(x => x.UserName));




            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Stock, StockDto>().ReverseMap();


            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<InvoiceDetail, InvoiceDetailDto>().ReverseMap();



            CreateMap<InvoiceDto, Invoice>()
               .ForMember(x => x.InvoiceDetails, opt => opt.MapFrom(x => x.Details.Select(g => new InvoiceDetail
               {
                   InvoiceId = x.Id,
                   StockId = g.StockId,
                   Price = g.Price,
                   Quantity = g.Quantity,
                   UnitPrice = g.UnitPrice,
               }
               ).ToList()));


            CreateMap<Invoice, InvoiceDto>()
                .ForMember(x => x.ClientName, g => g.MapFrom(x => x.Client.Name))
                .ForMember(x => x.Details, opt => opt.MapFrom(x => x.InvoiceDetails.Select(g => new InvoiceDetailDto
                {
                    Price = g.Price,
                    Quantity = g.Quantity,
                    StockId = g.StockId,
                    StockName = g.Stock.Name ?? "",
                    UnitPrice = g.UnitPrice,
                    Id = g.Id,
                }).ToList()));

        }
    }
}
