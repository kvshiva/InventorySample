using Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using InventorySampleServer.Model.Custom.Other;
using InventorySampleServer.Model.Part.Part;

namespace InventorySampleUI.Service
{
    public class PartService : BaseService
    {
        public PartService(IConfiguration configuration, IJSRuntime jsRuntime, NavigationManager UriHelper) : base(configuration, jsRuntime, UriHelper) { }
        public async Task<List<PartListDto>> GetPartListByStoreId(GetListParamsDto Params)
        {
            #region GetPartListByStoreId
            string url = BaseURL + "api/Part/GetListByStoreId?"+ ToQueryString(Params);
            var List = new List<PartListDto>();
            // Token = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "Token");
            // Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var Response = await Client.GetAsync(url);

            if (Response.IsSuccessStatusCode)
            {
                ApiResult = await Response.Content.ReadFromJsonAsync<ResultDto>();
                if (ApiResult.IsSucceed)
                {
                    List = JsonConvert.DeserializeObject<List<PartListDto>>(ApiResult.Data.ToString());
                }
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("/login");
            }
            //else
            //{
            //    ApiResult.IsSucceed = false;
            //    ApiResult.Message = "دریافت اطلاعات با خطا مواجه شد.";
            //    //Result.ErrorList.Add(new ErrorDto() { ErrorMessage = data.Content.ToString(), ErrorCode = Convert.ToInt32(data.StatusCode) });
            //}
            return List;
            #endregion
        }
    }
}
