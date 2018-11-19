using DalesTruckMaintenance.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalesTruckMaintenance.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        CustomerDto GetById(string customerId);
        CustomerDto CreateCustomer(CustomerDto customerDto);
        CustomerDto UpdateCustomer(CustomerDto customerDto);
    }
}
