﻿using System.Collections.Generic;

namespace Kledex.Domain
{
    public class SaveStoreData
    {
        public IDomainCommand Command { get; set; }
        public IList<IDomainEvent> Events { get; set; } = new List<IDomainEvent>();
        public IList<object> Properties { get; set; } = new List<object>();
    }
}
