using HR.LeaveManagment.MVC.Models;
using HR.LeaveManagment.MVC.Services.Base;

namespace HR.LeaveManagment.MVC.Contracts
{
    public interface ILeaveRequestService
    {
        Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList();
        Task<EmployeeLeaveRequestViewVM?> GetEmployeeLeaveRequests();
        Task<Response<int>>CreateLeaveRequest(CreateLeaveRequestVM createLeaveRequestVM);
        Task DeleteLeaveRequest (int Id);
        Task <LeaveRequestVM> GetLeaveRequestById(int Id);
        Task ApproveLeaveRequest(int id, bool approve);
    }
}
