
using HR.LeaveManagment.MVC.Contracts;
using HR.LeaveManagment.MVC.Services.Base;
using System.Diagnostics;

namespace HR.LeaveManagment.MVC.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        private readonly ILocalStorageServices _storageServices;
        private readonly IClient _client;
        public LeaveAllocationService(IClient client, ILocalStorageServices localstorage) : base(client, localstorage)
        {
            _storageServices = localstorage;
            _client = client;
        }
        public async Task<Response<int>> CreateLeaveAllocations(int LeaveTypeId)
        {
            try
            {
                Response<int> response = new();
                CreateLeveAllocationDto createLeveAllocation = new() { LeaveTypeId = LeaveTypeId};
                AddBearerTooken();
                var apiResponse = await _client.LeaveAllocationPOSTAsync(createLeveAllocation);
                if(apiResponse.Success)
                {
                    response.Sucess = true;
                }
                else
                {
                    foreach(var error in apiResponse.Errors)
                    {
                        response.ValidationErros += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiException<int>(ex);
            }
        }
    }
}
