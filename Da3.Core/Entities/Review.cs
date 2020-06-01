using System.ComponentModel.DataAnnotations;

namespace Da3.Core.Entities
{
    public class Review : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int Target { get; set; }
        public int By { get; set; }
        public Type Type { get; set; }
        public string Content { get; set; }
        public int Star { get; set; }
    }

    public enum Type
    {
        Employee,
        Employer
    }
}