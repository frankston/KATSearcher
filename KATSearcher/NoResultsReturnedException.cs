using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KATSearcher
{
    public class NoResultsReturnedException : Exception
    {
        public NoResultsReturnedException()
        { }

        public NoResultsReturnedException(string message)
            : base(message)
        { }

        public NoResultsReturnedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}