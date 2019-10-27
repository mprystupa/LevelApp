using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using LevelApp.DAL.Models.Base.Interfaces;

namespace LevelApp.DAL.Models.Base
{
    [ExcludeFromCodeCoverage]
    public class Entity<TKey> : IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        public DateTime? DateCreatedUtc { get; set; }
        public DateTime? DateModifiedUtc { get; set; }
        public DateTime? DateDeletedUtc { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? DeletedBy { get; set; }
    }
}
