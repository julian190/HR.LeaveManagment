using HR.LeaveManagment.Application.Contracts.Presistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Presistance.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LeaveManagmentDbContext _context;

        private  ILeaveTypeRepository _leaveTypeRepository;
        private  ILeaveAllocationRepository _leaveAllocationRepository;
        private  ILeaveRequestRepository _leaveRequestRepository;
        public UnitOfWork(LeaveManagmentDbContext context)
        {
            _context = context;
        }
        public ILeaveAllocationRepository LeaveAllocationRepository => _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);

        public ILeaveRequestRepository LeaveRequestRepository => _leaveRequestRepository ??=new LeaveRequesRepository(_context);

        public ILeaveTypeRepository LeaveTypeRepository => _leaveTypeRepository ??= new LeaveTypeRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
