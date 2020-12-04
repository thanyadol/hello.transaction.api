using System;
using AutoMapper;
using hello.transaction.core.Extensions;
using hello.transaction.core.Models;

namespace hello.transaction.core.Mapper
{
    public class TransactionXmlMapper : Profile
    {
        public TransactionXmlMapper()
        {
            CreateMap<TransactionXml, Transaction>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.PaymentDetails.Amount))
                    .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.PaymentDetails.CurrencyCode))

                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))

                    .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate))

                    //set xml
                    .ForMember(dest => dest.FormatType, opt => opt.MapFrom(src => FormatType.XML.GetDescription()))

                    .ReverseMap();

        }
    }
}