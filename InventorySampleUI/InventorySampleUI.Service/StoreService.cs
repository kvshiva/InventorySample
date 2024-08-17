using Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using InventorySampleServer.Model.Custom.InventoryVoucher;
using InventorySampleServer.Model.Custom.Other;
using InventorySampleServer.Model.Store.InventoryVoucher;
using InventorySampleServer.Model.Store.InventoryVoucherSpecification;
using InventorySampleServer.Model.Store.Store;

namespace InventorySampleUI.Service
{
    public class StoreService : BaseService
    {
        public StoreService(IConfiguration configuration, IJSRuntime jsRuntime, NavigationManager UriHelper) : base(configuration, jsRuntime, UriHelper) { }

        public async Task<List<InventoryVoucherListVM>> GetInventoryVoucherListCustom(GetListParamsDto Params)
        {
            #region GetInventoryVoucherListCustom
            string url = BaseURL + "api/InventoryVoucher/GetListCustom?"+ ToQueryString(Params);
            var Response = await Client.GetAsync(url);
            // Token = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "Token");
            // Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var Result = new List<InventoryVoucherListVM>();
            if (Response.IsSuccessStatusCode)
            {
                ApiResult = await Response.Content.ReadFromJsonAsync<ResultDto>();
                if (ApiResult.IsSucceed)
                {
                    Result = JsonConvert.DeserializeObject<List<InventoryVoucherListVM>>(ApiResult.Data.ToString());
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
            return Result;
            #endregion
        }
        public async Task<InventoryVoucherHDVM> GetInventoryVoucherByIdCustom(int Id)
        {
            #region GetInventoryVoucherByIdCustom
            string url = BaseURL + "api/InventoryVoucher/GetByIdCustom/" + Id;
            var Entity = new InventoryVoucherHDVM();
            // Token = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "Token");
            // Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var Response = await Client.GetAsync(url);

            if (Response.IsSuccessStatusCode)
            {
                ApiResult = await Response.Content.ReadFromJsonAsync<ResultDto>();
                if (ApiResult.IsSucceed)
                {
                    Entity = JsonConvert.DeserializeObject<InventoryVoucherHDVM>(ApiResult.Data.ToString());
                }
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("/login");
            }
            return Entity;
            #endregion
        }
        public async Task<HddInventoryVoucherDto> GetInventoryVoucherById(int Id)
        {
            #region GetInventoryVoucherById
            string url = BaseURL + "api/InventoryVoucher/GetById/" + Id;
            var Entity = new HddInventoryVoucherDto();
            // Token = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "Token");
            // Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var Response = await Client.GetAsync(url);

            if (Response.IsSuccessStatusCode)
            {
                ApiResult = await Response.Content.ReadFromJsonAsync<ResultDto>();
                if (ApiResult.IsSucceed)
                {
                    Entity = JsonConvert.DeserializeObject<HddInventoryVoucherDto>(ApiResult.Data.ToString());
                }
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("/login");
            }
            return Entity;
            #endregion
        }
        public async Task<List<InventoryVoucherSpecificationListDto>> GetInventoryVoucherSpecificationList(string? SearchValue = null, string? SortField = null, int? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
        {
            #region GetInventoryVoucherSpecificationList
            string url = BaseURL + "api/InventoryVoucherSpecification/GetList";
            var List = new List<InventoryVoucherSpecificationListDto>();
            // Token = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "Token");
            // Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var Response = await Client.GetAsync(url);

            if (Response.IsSuccessStatusCode)
            {
                ApiResult = await Response.Content.ReadFromJsonAsync<ResultDto>();
                if (ApiResult.IsSucceed)
                {
                    List = JsonConvert.DeserializeObject<List<InventoryVoucherSpecificationListDto>>(ApiResult.Data.ToString());
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
        public async Task<InventoryVoucherSpecificationListDto> GetInventoryVoucherSpecificationById(int Id)
        {
            #region GetInventoryVoucherSpecificationList
            string url = BaseURL + "api/InventoryVoucherSpecification/GetById/"+Id;
            var Entity = new InventoryVoucherSpecificationListDto();
            // Token = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "Token");
            // Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var Response = await Client.GetAsync(url);

            if (Response.IsSuccessStatusCode)
            {
                ApiResult = await Response.Content.ReadFromJsonAsync<ResultDto>();
                if (ApiResult.IsSucceed)
                {
                    Entity = JsonConvert.DeserializeObject<InventoryVoucherSpecificationListDto>(ApiResult.Data.ToString());
                }
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("/login");
            }
            return Entity;
            #endregion
        }
        public async Task<List<StoreListDto>> GetStoreList(string? SearchValue = null, string? SortField = null, int? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
        {
            #region GetStoreList
            string url = BaseURL + "api/Store/GetList";
            var List = new List<StoreListDto>();
            // Token = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "Token");
            // Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var Response = await Client.GetAsync(url);

            if (Response.IsSuccessStatusCode)
            {
                ApiResult = await Response.Content.ReadFromJsonAsync<ResultDto>();
                if (ApiResult.IsSucceed)
                {
                    List = JsonConvert.DeserializeObject<List<StoreListDto>>(ApiResult.Data.ToString());
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
        public async Task SaveReceipt(HddInventoryVoucherDto Entity)
        {
            #region SaveReceipt
            string url = BaseURL + "api/InventoryVoucher/" + (Entity.Id > 0 ? "EditReceipt" : "AddReceipt");
            // Token = await _jsRuntime.InvokeAsync<string>("localStorageHelper.getItem", "Token");
            // Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var json = JsonConvert.SerializeObject(Entity);
            var formdata = new MultipartFormDataContent
            {
                { new StringContent(json), "data" }
            };
            var Response = await Client.PostAsync(url, formdata);

            if (Response.IsSuccessStatusCode)
            {
                ApiResult = await Response.Content.ReadFromJsonAsync<ResultDto>();
                if (ApiResult.IsSucceed || ApiResult.ErrorList == null || ApiResult.ErrorList.Count == 0)
                    await _jsRuntime.InvokeVoidAsync("alert", ApiResult.Message);
                else // If there are error messages
                {
                    string ErrorMessages = string.Empty;
                    foreach(var err in ApiResult.ErrorList)
                    {
                        ErrorMessages += err.ErrorMessage + " ";
                    }
                    await _jsRuntime.InvokeVoidAsync("alert", ErrorMessages);
                }
                _navigationManager.NavigateTo("/");
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("/login");
            }
            #endregion
        }
    }
}
