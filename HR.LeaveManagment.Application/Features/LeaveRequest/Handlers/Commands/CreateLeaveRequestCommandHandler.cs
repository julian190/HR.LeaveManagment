using AutoMapper;
using HR.LeaveManagment.Application.Features.LeaveRequest.Requests.Commands;
using MediatR;
using HR.LeaveManagment.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagment.Application.Exceptions;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.Models;
using HR.LeaveManagment.Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using HR.LeaveManagment.Domain;
using HR.LeaveManagment.Application.Responses;
using System.Security.Claims;
using HR.LeaveManagment.Application.Constants;


namespace HR.LeaveManagment.Application.Features.LeaveRequest.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
       // private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
       /// private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _HttpContextAccessor;
       //private readonly ILeaveAllocationRepository _allocationRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender
            , IHttpContextAccessor httpContextAccessor, ILeaveAllocationRepository allocationRepository, IUnitOfWork unitOfWork)
        {
            // _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            // _leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
            _HttpContextAccessor = httpContextAccessor;
            //_allocationRepository = allocationRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse  response = new BaseCommandResponse();
            var LeaveRequestValidators = new CreateLeaveRequestDtoValidators(_unitOfWork.LeaveTypeRepository);
            var validaterequest = await LeaveRequestValidators.ValidateAsync(request.LeaveRequestDto);
            var userid = _HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == CustomClaimTypes.Uid)?.Value;
            var allocation = await _unitOfWork.LeaveAllocationRepository.GetLeaveAllocationByUserID(userid, request.LeaveRequestDto.LeaveTypeId);

            if (allocation is null)
            {
                validaterequest.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveRequestDto.LeaveTypeId), "You do not have any allocation for this leave type."));
            }
            else {
                int daysRequested = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;
                if (daysRequested > allocation.NumberOfDays)
                {
                    validaterequest.Errors.Add
                        (new FluentValidation.Results.ValidationFailure
                        (nameof(request.LeaveRequestDto.EndDate), "You don't have enought days for this request"));
                }
            }
            if (validaterequest.IsValid == false)
            {
                response.Success = false;
                response.Errors = validaterequest.Errors.Select(x => x.ErrorMessage).ToList();
               
            }

            var leaverequest = _mapper.Map<HR.LeaveManagment.Domain.LeaveRequest>(request.LeaveRequestDto);
            leaverequest.RequestingEmployeeId = userid;
            leaverequest = await _unitOfWork.LeaveRequestRepository.Add(leaverequest);
            await _unitOfWork.Save();
            response.Message = "Leave request submitted";
            response.Success = true;
            response.Id = leaverequest.Id;
            var emailAddress = _HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            //Send email logic
            Email emailobj = new Email();
            emailobj.To = emailAddress;
            emailobj.Subject = "Leave request submitted";
            emailobj.Body = $"Your leave request for {request.LeaveRequestDto.StartDate} to {request.LeaveRequestDto.EndDate} has been submitted sucessfully";
            try
            {
                await _emailSender.Sendemail(emailobj);
            }
            catch (Exception ex)
            {
                //log the error
            }
            return response;
        }
    }
}
