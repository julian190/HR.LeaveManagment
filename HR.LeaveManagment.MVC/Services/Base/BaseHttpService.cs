using HR.LeaveManagment.MVC.Contracts;

namespace HR.LeaveManagment.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected readonly ILocalStorageServices _localStorage;
        protected IClient _client;

        public BaseHttpService(IClient client, ILocalStorageServices localStorageServices)
        {
            _localStorage = localStorageServices;
            _client = client;

        }

        protected Response<Guid> ConvertApiException<Guid>(ApiException exception)
        {
            if (exception.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "Validation error have occured.", ValidationErros = exception.Response, Sucess = false };
            }
            if (exception.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "The Request item could not be found.", Sucess = false };
            }
            else
            {
                return new Response<Guid>() { Message = "Something went wrong,Please try again.", Sucess = false };
            }
        }
        protected void AddBearerTooken()
        {
            if (_localStorage.Exists("token"))
                _client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _localStorage.GetStorageValue<string>("token"));
        }
    }
}
