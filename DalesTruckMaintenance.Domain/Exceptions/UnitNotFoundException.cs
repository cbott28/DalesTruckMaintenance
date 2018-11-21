using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalesTruckMaintenance.Domain.Exceptions
{
    public class UnitNotFoundException : Exception, ISerializable
    {
        public UnitNotFoundException() : base()
        {
        }

        public UnitNotFoundException(string message) : base(message)
        {
        }

        public UnitNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UnitNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
