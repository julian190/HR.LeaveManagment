using AutoMapper;
using HR.LeaveManagment.MVC.Models;
using HR.LeaveManagment.MVC.Services.Base;

namespace HR.LeaveManagment.MVC
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto,CreateLeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDto,LeaveTypeVM>().ReverseMap();
            CreateMap<RegisterVM,RegistrationRequest>().ReverseMap();
        }
    }
}
