using AutoMapper;
using HR.LeaveManagment.Application.DTOs.LeaveAllocation;
using HR.LeaveManagment.Application.DTOs.LeaveRequest;
using HR.LeaveManagment.Application.DTOs.LeaveType;
using HR.LeaveManagment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HR.LeaveManagment.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestListDto>().ForMember(de=>de.DateRequested, x=>x.MapFrom(src=>src.DateCreated)).ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();


            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType,CreateLeaveTypeDto>().ReverseMap();
            CreateMap<LeaveRequest,CreateLeaveRequestDto>().ReverseMap();
        }

    }
}
