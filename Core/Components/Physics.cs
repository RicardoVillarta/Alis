﻿namespace Alis.Core
{
    using System.Diagnostics;


    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class Physics
    {
        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
