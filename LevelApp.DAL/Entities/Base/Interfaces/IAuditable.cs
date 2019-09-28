using System;
using System.Collections.Generic;
using System.Text;

namespace LevelApp.DAL.Entities.Base
{
    public interface IAuditable
    {
        DateTime? DateCreatedUtc { get; set; }
        DateTime? DateModifiedUtc { get; set; }
        DateTime? DateDeletedUtc { get; set; }
        int CreatedBy { get; set; }
        int? ModifiedBy { get; set; }
        int? DeletedBy { get; set; }
    }
}
