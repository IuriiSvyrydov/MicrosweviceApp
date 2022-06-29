using MicroserviceApp.Web;
using static MicroserviceApp.Web.SD;
namespace MicroserviceApp.Web.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

    }
}
