using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Exceptions
{
    public class SameBreedExcecption : Exception
    {
        public SameBreedExcecption(string message) : base(message)
        {
        }
    }
}
