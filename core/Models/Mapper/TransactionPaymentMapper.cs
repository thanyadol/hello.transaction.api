using System;
using AutoMapper;
using hello.transaction.core.Extensions;
using hello.transaction.core.Models;

namespace hello.transaction.core.Mapper
{
    public class TransactionPaymentMapper : Profile
    {
        public TransactionPaymentMapper()
        {
            CreateMap<Transaction, TransactionPayment>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ParseEnum<TransactionStatus>().GetDescription()))
                    .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Amount + " " + src.CurrencyCode))

                    .ReverseMap();

        }
    }
}