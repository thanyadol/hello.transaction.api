using System;

using System.ComponentModel;

namespace hello.transaction.core.Models
{
    // Add the attribute Flags or FlagsAttribute.
    [Flags]
    public enum AttachmentStatus
    {

        [Description("Valid")]
        VALID,


        [Description("Invalid")]
        INVALID,

    }
}