using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.DTO;
using Microsoft.Extensions.Configuration;

namespace InventorySampleUI.Service
{
    public class BaseService
    {
        protected HttpClient Client = new HttpClient();
        public string BaseURL;
        protected ResultDto ApiResult = new ResultDto() { ErrorList = new List<ErrorDto>() };
        protected string Token;
        protected NavigationManager _navigationManager;
        protected IJSRuntime _jsRuntime;

        public BaseService(IConfiguration configuration, IJSRuntime jsRuntime, NavigationManager UriHelper)
        {
            BaseURL = configuration.GetSection("BaseURL").Value ?? string.Empty;
            _navigationManager = UriHelper;
            _jsRuntime = jsRuntime;
        }
        public static string ToQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + Uri.EscapeDataString(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }

    }
}
