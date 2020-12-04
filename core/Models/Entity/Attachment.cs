
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hello.transaction.core.Models
{

    public class Attachment : Entity<string>
    {
        /// <summary>
        /// Base 64 content of file
        /// </summary>
        [Description("Base 64 content of file")]
        public byte[] Base64Content { get; set; }

        /// <summary>
        /// Name of file without extension
        /// </summary>
        [Description("Name of file without extension")]
        [StringLength(500)]
        public string Name { get; set; }

        /// <summary>
        /// File full path
        /// </summary>
        [Description("File full path")]
        [NotMapped]
        [StringLength(500)]
        public string Path { get; set; }


        /// <summary>
        /// Just title for whatever you want
        /// </summary>
        [Description("Just title for whatever you want")]
        [StringLength(500)]
        public string Title { get; set; }

        /// <summary>
        /// File size in KB
        /// </summary>
        [Description("File size in KB")]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal Size { get; set; }

        /// <summary>
        /// File extension
        /// </summary>
        [Description("File extension")]
        [StringLength(10)]
        public string Extension { get; set; }

        /// <summary>
        /// Status of process
        /// </summary>
        [Description("Status of process")]
        [StringLength(10)]
        public string Status { get; set; }

        /// <summary>
        /// Just description
        /// </summary>
        [Description("Just description")]
        [StringLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// Who post this record
        /// </summary>
        [Description("Who post this record")]
        [StringLength(50)]
        public string By { get; set; }

        /// <summary>
        /// "Checksum of the content
        /// </summary>
        [Description("Checksum of the content")]
        [StringLength(500)]
        public string Checksum { get; set; }

        /// <summary>
        /// Storage type, design for indicate the file strage, local, db or blobs or s3
        /// </summary>
        [Description("Storage type, design for indicate the file strage, local, db or blobs or s3")]
        [StringLength(50)]
        public string StorageType { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        [Description("Created date")]
        [Column(TypeName = "datetime")]
        public Nullable<DateTime> Date { get; set; }

        /// <summary>
        /// Transactions list that succesfull load into database
        /// </summary>
        [Description("Transactions list that succesfull load into database")]
        [NotMapped]
        public IEnumerable<Transaction> LoadedTransaction { get; set; }

    }

}