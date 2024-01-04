using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Presistance.Repository
{
    public class LeaveRequesRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly LeaveManagmentDbContext _context;

        public LeaveRequesRepository(LeaveManagmentDbContext context):base(context) 
        {
            _context = context;
        }
        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus)
        {
            leaveRequest.Approved = ApprovalStatus;
            _context.Entry(leaveRequest).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           // await _context.SaveChangesAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var LeaveRequests = await _context.leaveRequests
                .Include(q=>q.LeaveType)
                .ToListAsync();
            return LeaveRequests;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string id)
        {
            var leaverequests = await _context.leaveRequests
                .Where(u=>u.RequestingEmployeeId == id).Include(q=>q.LeaveType).ToListAsync();
            return leaverequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int Id)
        {
            var leaverequest = await _context.leaveRequests
                .Include(q=>q.LeaveType)
                .FirstOrDefaultAsync(q=>q.Id == Id);
            return leaverequest;
        }

        public Task<LeaveRequest> GetLeaveRequestWithDetails(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
