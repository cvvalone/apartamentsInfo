using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartamentsInfo.Formatting
{
    [Flags]
    public enum FormattingMode
    {
        None = 0x0,
        Statistic = 0x1,
        OwnersTable = 0x2,
        ApartamentsTable = 0x4,
        DataStrings = 0x8,
    }
}
