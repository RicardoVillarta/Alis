﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Alis.Core
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class Sprite : Component
    {
        public Sprite()
        {
            
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}