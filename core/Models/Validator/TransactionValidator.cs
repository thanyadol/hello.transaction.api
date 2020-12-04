using System;

using System.Collections.Generic;
using System.Linq;

//validator
using FluentValidation;
using FluentValidation.Results;
using hello.transaction.core.Extensions;
using hello.transaction.core.Models;

namespace hello.transaction.core.Validator
{
    public class TransactionValidator : AbstractValidator<Transaction>
    {
        public TransactionValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Amount).NotNull().NotEmpty();
            RuleFor(x => x.CurrencyCode).NotNull().NotEmpty();
            RuleFor(x => x.TransactionDate).NotNull().NotEmpty();
            RuleFor(x => x.Status).NotNull().NotEmpty();

            //try valid status
            RuleFor(y => y).Custom((dto, context) =>
            {
                if (true) //dto.FormatType == FormatType.CSV.GetDescription())
                {
                    TransactionStatus statusValue;
                    if (!Enum.TryParse(dto.Status, true, out statusValue))
                    {
                        context.AddFailure(new ValidationFailure($"Status", $"Invalid Status"));
                    }
                }

            });

        }
    }
}