using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string Name,Object Key): base($"{Name} ({Key}) was Not found")
        {
            
        }
    }
}
