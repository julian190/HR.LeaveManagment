using HR.LeaveManagment.MVC.Services.Base;

namespace HR.LeaveManagment.MVC.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<int>> CreateLeaveAllocations(int LeaveTypeId);
    }
}
