using DalesTruckMaintenance.Domain.Interfaces;
using DalesTruckMaintenance.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalesTruckMaintenance.Domain
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer GetCustomerById(string customerId)
        {
            var customerDto = _customerRepository.GetCustomerById(customerId);
            var customer = ConvertDtoToCustomer(customerDto);

            return customer;
        }

        public Customer CreateCustomer(Customer customer)
        {
            var customerDto = ConvertCustomerToDto(customer);
            customerDto = _customerRepository.CreateCustomer(customerDto);
            customer = ConvertDtoToCustomer(customerDto);

            return customer;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            var customerDto = ConvertCustomerToDto(customer);
            customerDto = _customerRepository.UpdateCustomer(customerDto);
            customer = ConvertDtoToCustomer(customerDto);

            return customer;
        }

        public IReadOnlyList<Customer> GetListOfCustomers()
        {
            var customerDtos = _customerRepository.GetListOfCustomers();
            var customers = new List<Customer>();

            foreach (var customerDto in customerDtos)
            {
                var customer = ConvertDtoToCustomer(customerDto);
                customers.Add(customer);
            }

            return customers;
        }

        private Customer ConvertDtoToCustomer(CustomerDto customerDto)
        {
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<CustomerDto, Customer>());
            var mapper = config.CreateMapper();
            var customer = mapper.Map<Customer>(customerDto);

            return customer;
        }

        private CustomerDto ConvertCustomerToDto(Customer customer)
        {
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDto>());
            var mapper = config.CreateMapper();
            var customerDto = mapper.Map<CustomerDto>(customer);

            return customerDto;
        }
    }
}
