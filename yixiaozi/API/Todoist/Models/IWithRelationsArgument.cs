using System;
using System.Collections.Generic;

namespace yixiaozi.API.Todoist.Models
{
    internal interface IWithRelationsArgument
    {
        void UpdateRelatedTempIds(IDictionary<Guid, long> map);
    }
}
