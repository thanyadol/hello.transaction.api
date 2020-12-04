using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hello.transaction.core.Models
{
    public class TransactionPayment
    {
        public string Id { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
    }

}