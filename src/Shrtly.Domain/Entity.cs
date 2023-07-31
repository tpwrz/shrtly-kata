using System.ComponentModel.DataAnnotations;

namespace ShrtLy.Domain
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
