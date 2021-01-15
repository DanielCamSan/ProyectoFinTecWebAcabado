using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Exceptions
{
    public class BadRequestException:Exception
    {
        public BadRequestException(string ex):base(ex)
        {

        }
    }
}
