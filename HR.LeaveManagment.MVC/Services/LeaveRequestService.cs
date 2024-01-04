using AutoMapper;
using Hanssens.Net;
using HR.LeaveManagment.MVC.Contracts;
using HR.LeaveManagment.MVC.Models;
using HR.LeaveManagment.MVC.Services.Base;
using System.Xml.Schema;

namespace HR.LeaveManagment.MVC.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        private readonly ILocalStorageServices _localStorageServices;
        private readonly IMapper _mapper;
        private readonly IClient _client;

        public LeaveRequestService(ILocalStorageServices localstorage, IMapper mapper, IClient client) : base(client, localstorage)
        {
            _localStorageServices = localstorage;
            _mapper = mapper;
            _client = client;
        }

        public async Task ApproveLeaveRequest(int id, bool approve)
        {
            AddBearerTooken();
            try
            {
                var request = new ChangeLeaveRequestApprovalDto { Id = id, Approved = approve };
                await _client.LeaveRequestPUT2Async(id,request.Approved.ToString(),request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM createLeaveRequestVM)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveRequestDto createLeaveRequest = _mapper.Map<CreateLeaveRequestDto>(createLeaveRequestVM);
                AddBearerTooken();
                var apirespnose = await _client.LeaveRequestPOSTAsync(createLeaveRequest);
                if (apirespnose.Success)
                {
                    response.Data = apirespnose.Id;
                    response.Sucess = true;
                }
                else
                {
                    foreach (var item in apirespnose.Errors)
                    {
                        response.ValidationErros += item + Environment.NewLine;
                    }
                }
                return response;
            }
            catch(ApiException ex)
            {
                return ConvertApiException<int>(ex);
            }
        }

        public Task DeleteLeaveRequest(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
        {
            AddBearerTooken();
            var leaveRequests = await _client.LeaveRequestAllAsync(false);
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
                PendingRequests = leaveRequests.Count(q => q.Approved == null),
                RejectedRequests = leaveRequests.Count(q => q.Approved == false),
                LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
            };
            return model;
        }

        public async Task<EmployeeLeaveRequestViewVM?> GetEmployeeLeaveRequests()
        {
            AddBearerTooken() ;
            var leaveRequests = await _client.LeaveRequestAllAsync(isLoggedInUser: true);
            var allocation = await _client.LeaveAllocationAllAsync(isLoggedInUser: true);
            var model = new EmployeeLeaveRequestViewVM
            {
                LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocation),
                LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
            };
            return model;
        }

        public async Task<LeaveRequestVM> GetLeaveRequestById(int Id)
        {
            AddBearerTooken();
            var leaveRequest = await _client.LeaveRequestGETAsync(Id);
            return _mapper.Map<LeaveRequestVM>(leaveRequest);
        }
    }
}
