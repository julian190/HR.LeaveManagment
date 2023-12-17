using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Presistance.Repository
{
    public class LeaveTypeRepository:GenericRepository<LeaveType>,ILeaveTypeRepository
    {
        private readonly LeaveManagmentDbContext _context;

        public LeaveTypeRepository(LeaveManagmentDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
