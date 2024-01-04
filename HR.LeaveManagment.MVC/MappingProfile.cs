using AutoMapper;
using HR.LeaveManagment.Domain;
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
            CreateMap<LeaveRequestDto, LeaveRequestVM>()
              .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
              .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
              .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
              .ReverseMap();

            CreateMap<CreateLeaveRequestDto,CreateLeaveRequestVM>()
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
                .ReverseMap();

            CreateMap<LeaveAllocationDto, LeaveAllocationVM>().ReverseMap();
            CreateMap<EmployeeVM, Employee>().ReverseMap();
        }
    }
}
