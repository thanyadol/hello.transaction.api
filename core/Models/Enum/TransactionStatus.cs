using System;

using System.ComponentModel;

namespace hello.transaction.core.Models
{
    // Add the attribute Flags or FlagsAttribute.
    [Flags]
    public enum TransactionStatus
    {

        [Description("A")]
        Approved,

        [Description("R")]
        Failed,

        [Description("D")]
        Finished,

        [Description("R")]
        Rejected,

        [Description("D")]
        Done,


    }
}