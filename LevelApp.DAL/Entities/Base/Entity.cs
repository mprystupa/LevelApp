using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LevelApp.DAL.Entities.Base
{
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
