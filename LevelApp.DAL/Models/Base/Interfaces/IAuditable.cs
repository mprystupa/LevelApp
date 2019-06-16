using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LevelApp.DAL.Models.Base
{
    public interface IAuditable
    {
        DateTime? DateCreatedUtc { get; set; }
        DateTime? DateModifiedUtc { get; set; }
        DateTime? DateDeletedUtc { get; set; }
        long CreatedBy { get; set; }
        long? ModifiedBy { get; set; }
        long? DeletedBy { get; set; }
    }
}
