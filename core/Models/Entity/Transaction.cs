using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hello.transaction.core.Models
{
    public class Transaction : Entity<string>
    {
        /// <summary>
        /// Amount in decimal, Decimal Number
        /// </summary>
        [Description("Amount in decimal, Decimal Number")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Currency code in with ISO4217
        /// </summary>
        [Description("Currency code in with ISO4217 format")]
        [StringLength(3)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Date of transaction occured, Format dd/MM/yyyy hh:mm:ss
        /// </summary>
        [Description("Date of transaction occured, Format dd/MM/yyyy hh:mm:ss")]
        [Column(TypeName = "datetime")]
        public Nullable<DateTime> TransactionDate { get; set; }

        /// <summary>
        /// Date of transaction occured, Choice of: 
        //    - Approved 
        ///   - Failed 
        ///   - Finished
        /// </summary>
        [Description("Date of transaction occured, see enum")]
        [StringLength(20)]
        public string Status { get; set; }

        /// <summary>
        /// Format type of loaded file, CSV or XML, see Enum
        /// </summary>
        [Description("Format type of loaded file, CSV or XML, see Enum")]
        [NotMapped]
        public string FormatType { get; set; }


        /// <summary>
        /// Id relate to atttachment
        /// </summary>
        [Description("Id relate to atttachment")]
        [ForeignKey("Attachment")]
        [StringLength(50)]
        public string AttachmentId { get; set; }



    }
}