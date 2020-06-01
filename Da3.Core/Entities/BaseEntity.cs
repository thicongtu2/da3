using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Da3.Core.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedTime { get; set; }
        
        public DateTime UpdateTime { get; set; }

        public int DelFlag { get; set; } = 0;
    }
}