using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalesTruckMaintenance.Domain.DTOs
{
    public class UnitDto
    {
        public string UnitId { get; set; }
        public string Description { get; set; }
        public double Mileage { get; set; }
    }
}
