using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Da3.Core.Entities
{
    public class Category : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public List<JobCategory> JobCategories { get; set; }
    }
}