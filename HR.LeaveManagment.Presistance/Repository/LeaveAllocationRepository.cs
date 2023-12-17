using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagment.Presistance.Repository
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly LeaveManagmentDbContext _leaveManagmentDbContext;

        public LeaveAllocationRepository(LeaveManagmentDbContext leaveManagmentDbContext):base(leaveManagmentDbContext)
        {
            _leaveManagmentDbContext = leaveManagmentDbContext;
        }
        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var LeaveAllocation = await _leaveManagmentDbContext.leaveAllocation
                .Include(q => q.LeaveType).FirstOrDefaultAsync(x => x.Id == id);
                
            return LeaveAllocation;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
        {
            var LeaveAllocations = await _leaveManagmentDbContext.leaveAllocation
                          .Include(q => q.LeaveType)
                          .ToListAsync();
            return LeaveAllocations;
        }
    }
}
