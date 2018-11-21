using System;
using DalesTruckMaintenance.Domain;
using DalesTruckMaintenance.Domain.DTOs;
using DalesTruckMaintenance.Domain.Exceptions;
using DalesTruckMaintenance.Domain.Interfaces;
using FakeItEasy;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DalesTruckMaintenance.Test
{
    [TestClass]
    public class CustomerTest
    {
        private ICustomerRepository _customerRepository;
        private CustomerService _customerService;
        private const string ValidCustomerId = "VALIDCUSTOMERID";
        private const string InvalidCustomerId = "INVALIDCUSTOMERID";
        private const string ValidCustomerName = "VALIDCUSTOMERNAME";

        private Customer ValidCustomer = new Customer()
        {
            CustomerId = ValidCustomerId,
            Name = ValidCustomerName
        };

        private Customer InvalidCustomer = new Customer()
        {
            CustomerId = InvalidCustomerId,
            Name = ""
        };

        [TestInitialize]
        public void Initialize()
        {
            _customerRepository = A.Fake<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepository);

            A.CallTo(() => _customerRepository.GetCustomerById(ValidCustomerId)).Returns(new CustomerDto()
            {
                CustomerId = ValidCustomerId,
                Name = ValidCustomerName
            });


            A.CallTo(() => _customerRepository.GetCustomerById(InvalidCustomerId)).Throws(new CustomerNotFoundException());

            A.CallTo(() => _customerRepository.CreateCustomer(A<CustomerDto>.That.Matches(x => x.Name == ValidCustomerName)))
                .Returns(new CustomerDto()
                {
                    CustomerId = Guid.NewGuid().ToString(),
                    Name = ValidCustomerName
                });

            A.CallTo(() => _customerRepository.CreateCustomer(A<CustomerDto>.That.Matches(x => x.Name == null || x.Name.Length == 0)))
                .Throws(new InvalidCustomerException());

            A.CallTo(() => _customerRepository.UpdateCustomer(A<CustomerDto>.That.Matches(x => x.CustomerId == ValidCustomerId &&
                x.Name == ValidCustomerName))).Returns(new CustomerDto()
                {
                    CustomerId = ValidCustomerId,
                    Name = "UPDATEDNAME"
                });

            A.CallTo(() => _customerRepository.UpdateCustomer(A<CustomerDto>.That.Matches(x => x.CustomerId == InvalidCustomerId ||
                x.Name == null || x.Name.Length == 0))).Throws(new InvalidCustomerException());

            A.CallTo(() => _customerRepository.GetListOfCustomers()).Returns(new List<CustomerDto>() {
                new CustomerDto() {
                    CustomerId = ValidCustomerId,
                    Name = ValidCustomerName
                }
            });
        }

        [TestMethod]
        public void GetCustomerById_ValidId_ReturnsCustomer()
        {
            //Act
            var customer = _customerService.GetCustomerById(ValidCustomerId);

            //Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Assert.IsTrue(customer.CustomerId == ValidCustomerId);
            Assert.IsNotNull(customer.Name);
        }

        [TestMethod, ExpectedException(typeof(CustomerNotFoundException))]
        public void GetCustomerById_InvalidId_ThrowsException()
        {
            //Act
            var customer = _customerService.GetCustomerById(InvalidCustomerId);
        }

        [TestMethod]
        public void CreateCustomer_ValidCustomer_CreatesCustomer()
        {
            //Act
            var customer = _customerService.CreateCustomer(ValidCustomer);

            //Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Guid guidOut;
            Assert.IsTrue(Guid.TryParse(customer.CustomerId, out guidOut));
        }

        [TestMethod, ExpectedException(typeof(InvalidCustomerException))]
        public void CreateCustomer_InvalidCustomer_ThrowsException()
        {
            //Act
            var customer = _customerService.CreateCustomer(InvalidCustomer);

            //Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Guid guidOut;
            Assert.IsTrue(Guid.TryParse(customer.CustomerId, out guidOut));
        }

        [TestMethod]
        public void UpdateCustomer_ValidCustomer_UpdatesCustomer()
        {
            //Act
            var customer = _customerService.UpdateCustomer(ValidCustomer);

            //Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Assert.IsTrue(customer.CustomerId == ValidCustomer.CustomerId);
            Assert.IsTrue(customer.Name.Length > 0);
        }

        [TestMethod, ExpectedException(typeof(InvalidCustomerException))]
        public void UpdateCustomer_InvalidCustomer_ThrowsException()
        {
            //Act
            var customer = _customerService.UpdateCustomer(InvalidCustomer);

            //Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Assert.IsTrue(customer.CustomerId == ValidCustomer.CustomerId);
            Assert.IsTrue(customer.Name.Length > 0);
        }

        [TestMethod]
        public void GetListOfCustomers_Void_ReturnsCustomers()
        {
            //Act
            var customers = _customerService.GetListOfCustomers();

            //Assert
            Assert.IsInstanceOfType(customers, typeof(IReadOnlyList<Customer>));
        }
    }
}
