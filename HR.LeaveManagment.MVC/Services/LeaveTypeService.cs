using AutoMapper;
using HR.LeaveManagment.MVC.Contracts;
using HR.LeaveManagment.MVC.Models;
using HR.LeaveManagment.MVC.Services.Base;
using System.Net.Http;

namespace HR.LeaveManagment.MVC.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly ILocalStorageServices _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpclient;
        public LeaveTypeService(IMapper mapper,IClient client, ILocalStorageServices localStorageServices) : base(client, localStorageServices)
        {
            this._localStorageService = localStorageServices;
            this._mapper = mapper;
            this._httpclient = client;
        }

        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveTypeDto createLeaveType = _mapper.Map<CreateLeaveTypeDto>(leaveType);
                AddBearerTooken();
                var apiResonse = await _client.LeaveTypesPOSTAsync(createLeaveType);
                if (apiResonse.Success)
                {
                    response.Data = apiResonse.Id;
                    response.Sucess = true;
                }
                else
                {
                    foreach (var error in apiResonse.Errors)
                    {
                        response.ValidationErros += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch(ApiException ex)
            {
                return ConvertApiException<int>(ex);
            }
        }

       

        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                AddBearerTooken();
                await _client.LeaveTypesDELETEAsync(id,null);
                return new Response<int> { Sucess = true };
            }
            catch(ApiException ex)
            {
                return ConvertApiException<int>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
            AddBearerTooken();
            var leaveType = await _client.LeaveTypesGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            AddBearerTooken();
            var leaveTypes = await _client.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            try
            {
                AddBearerTooken();
                LeaveTypeDto leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);
                await _client.LeaveTypesPUTAsync(id, leaveTypeDto);
                return new Response<int> { Sucess = true };
            }catch(ApiException ex)
            {
                return ConvertApiException<int>(ex) ;
            }
        }

    }
}
