using System.ComponentModel.DataAnnotations;

namespace Da3.Core.Entities
{
    public class Apply : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        public Job Job { get; set; }
        public int JobId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
    }
}