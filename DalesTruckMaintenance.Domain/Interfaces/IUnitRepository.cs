using DalesTruckMaintenance.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalesTruckMaintenance.Domain.Interfaces
{
    public interface IUnitRepository
    {
        UnitDto GetUnitById(string unitId);
        UnitDto CreateUnit(UnitDto unitDto);
        UnitDto UpdateUnit(UnitDto unitDto);
        IReadOnlyList<UnitDto> GetListOfUnits();
    }
}
