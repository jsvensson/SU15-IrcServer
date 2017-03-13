using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrcServer
{
    public enum NumericReply
    {
        NameReply = 353,
        EndOfNames = 366,
        NoSuchChannel = 403,
        ErroneousNickname = 432
    }
}
