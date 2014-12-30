using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Type of hook. After (precall), or before (postcall) making the real call.
    /// </summary>
    public enum HookType
    {
        PreCall,
        PostCall
    }
}
