using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hello.transaction.core.Models
{

    public class TransactionDto
    {
        public TransactionXmlParent Transactions;
    }
    public class TransactionXmlParent
    {
        public TransactionXml[] Transaction;
    }

    public class TransactionXml
    {
        [JsonProperty("@id")]
        public string Id { get; set; }
        public PaymentDetail PaymentDetails;
        public Nullable<DateTime> TransactionDate { get; set; }
        public string Status { get; set; }
    }

    public class PaymentDetail
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}