using System.ComponentModel.DataAnnotations;

namespace Da3.Core.Entities
{
    public class JobCategory : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public Category Category { get; set; }
    }
}