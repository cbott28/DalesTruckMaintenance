using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalesTruckMaintenance.Domain.Exceptions
{
    public class InvalidCustomerException : Exception, ISerializable
    {
        public InvalidCustomerException() : base()
        {
        }

        public InvalidCustomerException(string message) : base(message)
        {
        }

        public InvalidCustomerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidCustomerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
