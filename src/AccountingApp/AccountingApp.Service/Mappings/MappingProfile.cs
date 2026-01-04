using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using AccountingApp.Core.Entities;
using AutoMapper;

namespace AccountingApp.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Account,AccountDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();

            CreateMap<Invoice, InvoiceDto>()
               .ReverseMap()
               .ForMember(dest => dest.Customer, opt => opt.Ignore());

            CreateMap<TransactionLine, TransactionLineDto>()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.AccountCode, opt => opt.MapFrom(src => src.Account.Code));
        }
    }
}
