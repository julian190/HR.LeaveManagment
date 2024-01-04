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

        public async Task AddAllocation(List<LeaveAllocation> allocations)
        {
            await _leaveManagmentDbContext.AddRangeAsync(allocations);
            //await _leaveManagmentDbContext.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int LeaveTypeId, int Period)
        {
            return await _leaveManagmentDbContext.leaveAllocation.AnyAsync(q => q.EmployeeId == userId
            && q.LeaveTypeId == LeaveTypeId
            && q.Period == Period);
        }

        public async Task<LeaveAllocation> GetLeaveAllocationByUserID(string userId, int LeaveTypeId)
        {
           return await _leaveManagmentDbContext.leaveAllocation.FirstOrDefaultAsync(x=>x.EmployeeId == userId && x.LeaveTypeId == LeaveTypeId);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            return await _leaveManagmentDbContext.leaveAllocation.Where(q=>q.EmployeeId == userId).ToListAsync();
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
