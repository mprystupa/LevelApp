using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LevelApp.DAL.Models.Base
{
    public class Entity<TKey>: IAuditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        public DateTime? DateCreatedUtc { get; set; }
        public DateTime? DateModifiedUtc { get; set; }
        public DateTime? DateDeletedUtc { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? DeletedBy { get; set; }
    }
}
