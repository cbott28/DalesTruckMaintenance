using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using DalesTruckMaintenance.Domain;
using DalesTruckMaintenance.Domain.Interfaces;
using DalesTruckMaintenance.Domain.DTOs;
using DalesTruckMaintenance.Domain.Exceptions;
using System.Collections.Generic;

namespace DalesTruckMaintenance.Test
{
    [TestClass]
    public class UnitTest
    {
        private IUnitRepository _unitRepository;
        private UnitService _unitService;

        private const string ValidUnitId = "VALIDUNITID";
        private const string InvalidUnitId = "INVALIDUNITID";
        private const string ValidUnitDescription = "VALIDUNITDESCRIPTION";

        private Unit ValidUnit = new Unit()
        {
            UnitId = ValidUnitId,
            Description = ValidUnitDescription,
            Mileage = 0.0
        };

        private Unit InvalidUnit = new Unit()
        {
            Description = "",
            Mileage = -1.0
        };

        [TestInitialize]
        public void Initialize()
        {
            _unitRepository = A.Fake<IUnitRepository>();

            _unitService = new UnitService(_unitRepository);

            A.CallTo(() => _unitRepository.GetUnitById(ValidUnitId)).Returns(new UnitDto()
            {
                UnitId = ValidUnitId
            });

            A.CallTo(() => _unitRepository.GetUnitById(InvalidUnitId)).Throws(new UnitNotFoundException());

            A.CallTo(() => _unitRepository.CreateUnit(A<UnitDto>.That.Matches(x => x.Description == ValidUnitDescription))).
                Returns(new UnitDto() {
                    UnitId = Guid.NewGuid().ToString(),
                    Description = ValidUnitDescription,
                    Mileage = 0.0
                });

            A.CallTo(() => _unitRepository.CreateUnit(A<UnitDto>.That.Matches(x => x.Description.Length == 0))).
                Throws(new InvalidUnitException());

            A.CallTo(() => _unitRepository.UpdateUnit(A<UnitDto>.That.Matches(x => x.Description.Length > 0))).
                Returns(new UnitDto() {
                    UnitId = ValidUnitId,
                    Description = "UPDATEDUNITDESCRIPTION",
                    Mileage = 0.0
                });

            A.CallTo(() => _unitRepository.UpdateUnit(A<UnitDto>.That.Matches(x => x.Description.Length == 0 ||
                x.Mileage < 0.0))).Throws(new InvalidUnitException());

            A.CallTo(() => _unitRepository.GetListOfUnits()).Returns(new List<UnitDto>() {
                new UnitDto() {
                    UnitId = ValidUnitId,
                    Description = ValidUnitDescription,
                    Mileage = 0.0 }
            });
        }

         [TestMethod]
         public void GetUnitById_ValidUnitId_ReturnsUnit()
        {
            //Act
            var unit = _unitService.GetUnitById(ValidUnitId);

            //Assert
            Assert.IsInstanceOfType(unit, typeof(Unit));
            Assert.AreEqual(unit.UnitId, ValidUnitId);
         }

        [TestMethod, ExpectedException(typeof(UnitNotFoundException))]
        public void GetUnitById_InvalidUnitId_ThrowsException()
        {
            //Act
            var unit = _unitRepository.GetUnitById(InvalidUnitId);
        }

        [TestMethod]
        public void CreateUnit_ValidUnit_CreatesUnit()
        {
            //Act
            var unit = _unitService.CreateUnit(ValidUnit);

            //Assert
            Assert.IsInstanceOfType(unit, typeof(Unit));
            Guid guidOut;
            Assert.IsTrue(Guid.TryParse(unit.UnitId, out guidOut));
            Assert.IsTrue(unit.Description.Length > 0);
            Assert.IsTrue(unit.Mileage >= 0.0);
        }

        [TestMethod, ExpectedException(typeof(InvalidUnitException))]
        public void CreateUnit_InvalidUnit_ThrowsException()
        {
            //Act
            var unit = _unitService.CreateUnit(InvalidUnit);
        }

        [TestMethod]
        public void UpdateUnit_ValidUnit_UpdatesUnit()
        {
            //Act
            var unit = _unitService.UpdateUnit(ValidUnit);

            //Assert
            Assert.IsInstanceOfType(unit, typeof(Unit));
            Assert.AreEqual(unit.UnitId, ValidUnit.UnitId);
            Assert.IsTrue(unit.Description.Length > 0);
        }

        [TestMethod, ExpectedException(typeof(InvalidUnitException))]
        public void UpdateUnit_InvalidUnit_ThrowsException()
        {
            //Act
            var unit = _unitService.UpdateUnit(InvalidUnit);
        }

        [TestMethod]
        public void GetListOfUnits_Void_ReturnsListOfUnits()
        {
            //Act
            var units = _unitService.GetListOfUnits();

            //Assert
            Assert.IsInstanceOfType(units, typeof(IReadOnlyList<Unit>));
        }
    }
}
