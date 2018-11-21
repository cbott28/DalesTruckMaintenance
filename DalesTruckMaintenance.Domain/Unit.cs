using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalesTruckMaintenance.Domain
{
    public class Unit
    {
        public string UnitId { get; set; }
        public string Description { get; set; }
        public double Mileage { get; set; }
    }
}