using DalesTruckMaintenance.Domain.DTOs;
using DalesTruckMaintenance.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalesTruckMaintenance.Domain
{
    public class UnitService
    {
        private IUnitRepository _unitRepository;
        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public Unit GetUnitById(string unitId)
        {
            var unitDto = _unitRepository.GetUnitById(unitId);
            var unit = ConvertUnitDtoToUnit(unitDto);
            return unit;
        }

        public Unit CreateUnit(Unit unit)
        {
            var unitDto = ConvertUnitToUnitDto(unit);
            unitDto = _unitRepository.CreateUnit(unitDto);
            unit = ConvertUnitDtoToUnit(unitDto);
            return unit;
        }

        public Unit UpdateUnit(Unit unit)
        {
            var unitDto = ConvertUnitToUnitDto(unit);
            unitDto = _unitRepository.UpdateUnit(unitDto);
            unit = ConvertUnitDtoToUnit(unitDto);

            return unit;
        }

        public IReadOnlyList<Unit> GetListOfUnits()
        {
            var units = new List<Unit>();
            var unitDtos = _unitRepository.GetListOfUnits();

            foreach (var unitDto in unitDtos)
            {
                var unit = ConvertUnitDtoToUnit(unitDto);
                units.Add(unit);
            }

            return units;
        }

        private Unit ConvertUnitDtoToUnit(UnitDto unitDto)
        {
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<UnitDto, Unit>());
            var mapper = config.CreateMapper();
            var unit = mapper.Map<Unit>(unitDto);
            return unit;
        }

        private UnitDto ConvertUnitToUnitDto(Unit unit)
        {
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Unit, UnitDto>());
            var mapper = config.CreateMapper();
            var unitDto = mapper.Map<UnitDto>(unit);
            return unitDto;
        }
    }
}
