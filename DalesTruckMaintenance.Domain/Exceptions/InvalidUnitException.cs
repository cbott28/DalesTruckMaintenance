using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalesTruckMaintenance.Domain.Exceptions
{
    public class InvalidUnitException : Exception, ISerializable
    {
        public InvalidUnitException() : base()
        {
        }

        public InvalidUnitException(string message) : base(message)
        {
        }

        public InvalidUnitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidUnitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
