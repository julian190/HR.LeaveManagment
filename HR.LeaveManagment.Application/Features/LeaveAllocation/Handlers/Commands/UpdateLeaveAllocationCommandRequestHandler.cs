using AutoMapper;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.DTOs.LeaveAllocation.Validations;
using HR.LeaveManagment.Application.Exceptions;
using HR.LeaveManagment.Application.Features.LeaveAllocation.Requests.Commands;
using HR.LeaveManagment.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.Features.LeaveAllocation.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandRequestHandler:IRequestHandler<UpdateLeaveAllocationCommandRequest, int>
    {
       // private readonly ILeaveAllocationRepository _LeaveAllocationRepository;
        private readonly IMapper _mapper;
      //  private readonly ILeaveTypeRepository _leaveTypeRepository;
      private readonly IUnitOfWork _unitOfWork;

        public UpdateLeaveAllocationCommandRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateLeaveAllocationCommandRequest request, CancellationToken cancellationToken)
        {
            var validation = new UpdateLeaveAllocationDtoValidation(_unitOfWork.LeaveTypeRepository);
            var validationResult = validation.Validate(request.LeaveAllocationDto);
            if(validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var leaveallocation = await _unitOfWork.LeaveAllocationRepository.Get(request.LeaveAllocationDto.Id);
            _mapper.Map(request.LeaveAllocationDto, leaveallocation);
            await _unitOfWork.LeaveAllocationRepository.Update(leaveallocation);
            await _unitOfWork.Save();
            return 1;

        }
    
    }
}
