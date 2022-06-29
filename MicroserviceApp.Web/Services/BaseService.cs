using System.Text;
using MicroserviceApp.Web.Models;
using MicroserviceApp.Web.Services.IServices;
using Newtonsoft.Json;

namespace MicroserviceApp.Web.Services
{
    public class BaseService: IBaseService
    {
        public ResponseDto ResponseModelDto { get; set; }
        public IHttpClientFactory HttpClient { get; set; }
        public BaseService( IHttpClientFactory httpClient)
        {
           this. ResponseModelDto = new ResponseDto();
            HttpClient = httpClient;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public ResponseDto ResponseModel { get; set; }
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("MicroserviceAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept","application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                HttpResponseMessage apiResponseMessage = null;

                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                    case SD.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiResponseMessage = await client.SendAsync(message);
                var apiContent = await apiResponseMessage.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    DisplayMessages = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) }
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
          
        }
    } 
}
