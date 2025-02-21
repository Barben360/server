﻿using System.Collections.Generic;
using Bit.Core.Models.Table;
using Core.Models.Data;

namespace Bit.Core.Models.Data
{
    public class EmergencyAccessViewData
    {
        public EmergencyAccess EmergencyAccess { get; set; }
        public IEnumerable<CipherDetails> Ciphers { get; set; }
    }
}
