
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hello.transaction.core.Models
{

    public class Attachment
    {

        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        //[StringLength(50)]
        public string Base64Content { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [NotMapped]
        [StringLength(500)]
        public string Path { get; set; }


        [StringLength(500)]
        public string Title { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal Size { get; set; }

        [StringLength(10)]
        public string Extension { get; set; }


        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }


        [StringLength(50)]
        public string By { get; set; }

        [StringLength(500)]
        public string Checksum { get; set; }


        [StringLength(50)]
        public string StorageType { get; set; }


        [Column(TypeName = "datetime")]
        public Nullable<DateTime> Date { get; set; }

        [NotMapped]
        public IEnumerable<Transaction> LoadedTransaction { get; set; }

    }

}