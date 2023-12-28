using HR.LeaveManagment.MVC.Models;
using HR.LeaveManagment.MVC.Services.Base;

namespace HR.LeaveManagment.MVC.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVM>> GetLeaveTypes();
        Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
        Task <Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType);
        Task<Response<int>> UpdateLeaveType(int id,LeaveTypeVM leaveType);
        Task<Response<int>> DeleteLeaveType(int id);
    
    }
}
