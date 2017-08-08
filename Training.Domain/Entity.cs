using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Training.Domain
{
    public abstract class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
