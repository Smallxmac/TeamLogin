using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Enums
{
    /// <summary>
    /// Account Permissions to help figure out who the client is.
    /// EX if the account is staff or not.
    /// </summary>
    public enum AccountPermission : byte
    {
        Error,
        Normal,
        Moderator,
        Admin        
    }
}
