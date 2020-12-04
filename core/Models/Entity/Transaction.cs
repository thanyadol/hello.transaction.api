using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hello.transaction.core.Models
{
    public class Transaction
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        // with ISO4217
        [StringLength(3)]
        public string CurrencyCode { get; set; }


        [Column(TypeName = "datetime")]
        public Nullable<DateTime> TransactionDate { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [NotMapped]
        public string FormatType { get; set; }


        [ForeignKey("Attachment")]
        [StringLength(50)]
        public string AttachmentId { get; set; }



    }
}