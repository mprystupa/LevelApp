using System;

namespace LevelApp.DAL.Models.Base.Interfaces
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
