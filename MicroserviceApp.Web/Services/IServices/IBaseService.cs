using MicroserviceApp.Web.Models;
using MicroservicesApp.Product.API.Dtos;
using ResponseDto = MicroserviceApp.Web.Models.ResponseDto;

namespace MicroserviceApp.Web.Services.IServices
{
    public interface IBaseService: IDisposable
    {
        ResponseDto ResponseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest); 
    } 
}
