using HR.LeaveManagment.MVC.Models;

namespace HR.LeaveManagment.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task <bool> Authenticate(string email, string password);
        Task <bool> Register(RegisterVM register);
        Task logout();
    }
}
