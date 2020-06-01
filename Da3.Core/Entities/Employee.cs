using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Da3.Core.Entities
{
    public class Employee : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Locations { get; set; }
        public double Star { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Locations
    {
        public IList<Location> LocationsTracks { get; set; }
    }
}