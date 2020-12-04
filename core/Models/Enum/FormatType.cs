using System;

using System.ComponentModel;

namespace hello.transaction.core.Models
{
    // Add the attribute Flags or FlagsAttribute.
    [Flags]
    public enum FormatType
    {

        [Description("csv")]
        CSV,


        [Description("xml")]
        XML
    }
}