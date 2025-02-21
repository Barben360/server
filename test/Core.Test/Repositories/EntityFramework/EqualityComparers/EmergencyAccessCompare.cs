﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Bit.Core.Models.Table;

namespace Bit.Core.Test.Repositories.EntityFramework.EqualityComparers
{
    public class EmergencyAccessCompare : IEqualityComparer<EmergencyAccess>
    {
        public bool Equals(EmergencyAccess x, EmergencyAccess y)
        {
            return x.Email == y.Email &&
                x.KeyEncrypted == y.KeyEncrypted &&
                x.Type == y.Type &&
                x.Status == y.Status &&
                x.WaitTimeDays == y.WaitTimeDays &&
                x.RecoveryInitiatedDate == y.RecoveryInitiatedDate &&
                x.LastNotificationDate == y.LastNotificationDate;
        }

        public int GetHashCode([DisallowNull] EmergencyAccess obj)
        {
            return base.GetHashCode();
        }
    }
}
