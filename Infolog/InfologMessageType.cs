using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    [Flags]
    public enum InfologMessageType
    {
        None,
        Info,
        Warning,
        Error
    }
}
