using System.ComponentModel.DataAnnotations.Schema;

namespace ShrtLy.Domain
{
    [Table("Links")]
    public class LinkEntity : Entity
    {
        public string Url { get; set; }
        public string ShortUrl { get; set; }
    }
}
