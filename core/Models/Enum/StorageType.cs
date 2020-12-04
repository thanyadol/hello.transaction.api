using System;

using System.ComponentModel;

namespace hello.transaction.core.Models
{
    // Add the attribute Flags or FlagsAttribute.
    [Flags]
    public enum StorageType
    {

        [Description("DB")]
        DB,


        [Description("Local")]
        Local,


        [Description("S3")]
        S3,

    }
}