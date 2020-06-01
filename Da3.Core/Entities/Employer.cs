using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Da3.Core.Entities
{
    public class Employer : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public Da3User User { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(15)]
        [RegularExpression("([0-9]+)")] 
        public string Phone { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        public double Rate { get; set; }
        
        [MaxLength(2000)]
        public string Description { get; set; }
    }
}