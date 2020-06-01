using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Da3.Core.Entities
{
    public class Job : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        [Required]
        [MaxLength(100)]
        public string Salary { get; set; }
        public List<JobCategory> JobCategories { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }
        
        [NotMapped]
        public string Expired => String.Format($"Expired At "+CreatedTime.AddDays(3));
    }
}