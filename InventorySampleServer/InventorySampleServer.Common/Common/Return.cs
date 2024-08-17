using Common.DTO;
using Common.Enum;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.Results;

//using Microsoft.Data.SqlClient;
using System.Collections;

namespace Common
{
    public enum StatusType
    {
        ثبت = 0,
        حذف = 1,
        دریافت = 2
    }

    public enum StatusCode
    {
        // Information responses
        Continue = 100,
        Switching_Protocols = 101,
        Processing = 102,
        Early_Hints = 103,
        // Successful responses
        Ok = 200,
        Created = 201,
        Accepted = 202,
        Non_Authoritative_Information = 203,
        No_Content = 204,
        Reset_Content = 205,
        Partial_Content = 206,
        Multi_Status = 207,
        Already_Reported = 208,
        IM_Used = 226,
        // Redirection messages
        Multiple_Choices = 300,
        Moved_Permanently = 301,
        Found = 302,
        See_Other = 303,
        Not_Modified = 304,
        Use_Proxy = 305,
        Unused = 306,
        Temporary_Redirect = 307,
        Permanent_Redirect = 308,
        // Client error responses
        Bad_Request = 400,
        Unauthorized = 401,
        Payment_Required = 402,
        Forbidden = 403,
        Not_Found = 404,
        Method_Not_Allowed = 405,
        Not_Acceptable = 406,
        Proxy_Authentication_Required = 407,
        Request_Timeout = 408,
        Conflict = 409,
        Gone = 410,
        Length_Required = 411,
        Precondition_Failed = 412,
        Payload_Too_Large = 413,
        URI_Too_Long = 414,
        Unsupported_Media_Type = 415,
        Expectation_Failed = 417,
        Unprocessable_Content = 422,
        Locked = 423,
        Request_Header_Fields_Too_Large = 431,
        Unavailable_For_Legal_Reasons = 451,
        // Server error responses
        Internal_Server_Error = 500,
        Not_Implemented = 501,
        Bad_Gateway = 502,
        Service_Unavailable = 503,
        Gateway_Timeout = 504,
        HTTP_Version_Not_Supported = 505,
        Variant_Also_Negotiates = 506,
        Insufficient_Storage = 507,
        Loop_Detected = 508,
        Not_Extended = 510,
        Network_Authentication_Required = 511
    }

    public enum SqlExceptionKeys
    {
        خطایی_در_پایگاه_داده_رخ_داده_است = -400,
        شبکه_قطع_است = -10050,
        شبکه_در_دسترس_نمی_باشد = -10051,
        فضای_بافری_در_دسترس_نیست = -10055,
        ارتباط_با_پایگاه_داده_منقضی_شد = -10060,
        ارتباط_با_پایگاه_داده_رد_شد = -10061,
        سرور_خاموش_است = -10064,
        تلاش_برای_باز_کردن_اشاره_گری_که_از_قبل_باز_بوده_است = -101,
        عملیات_روی_اشاره_گر_بازنشده_انجام_شد = -102,
        عملیات_ویرایش_یا_حذف_انجام_شد_اما_اشاره_گر_روی_هیچ_ردیفی_قرار_نگرفت = -103,
        اعتبار_سنجی_فیلد_در_درج_اطلاعات_در_سمت_پایگاه_داده_با_خطا_مواجه_شد = -104,
        اعتبار_سنجی_فیلد_در_ویرایش_اطلاعات_در_سمت_پایگاه_داده_با_خطا_مواجه_شد = -105,
        ردیفی_برای_حذف_یافت_نشد = -106,
        فیلد_الزامی_وجود_ندارد_درج_یا_ویرایش_مجاز_نیست = -108,
        نمی_توان_ردیف_تعیین_شده_برای_ویرایش_را_پیدا_کرد = -109,
        دسترسی_غیر_مجاز = -112,
        رکورد_یا_رکوردهای_انتخابی_توسط_کاربر_دیگری_قفل_شده_است = -114,
        عملیات_درج_یا_ویرایش_یا_حذف_بر_روی_جدول_فقط_خواندنی_مقدور_نمی_باشد = -115,
        عدم_تطابق_اصلی_در_بین_لیست_مقادیر_و_تعداد_ستون_های_جدول = -116,
        توابع_تجمعی_در_نماها_پشتیبانی_نمی_شوند = -117,
        قید_منحصر_به_فرد_یا_قید_کلید_اصلی_برای_عملیات_درج_ناموفق_بوده_است = -119,
        قید_منحصر_به_فرد_یا_قید_کلید_اصلی_برای_عملیات_ویرایش_ناموفق_بوده_است = -120,
        به_علت_تناقض_کلید_خارجی_عملیات_درج_ممکن_نشد = -121,
        به_علت_تناقض_کلید_خارجی_عملیات_ویرایش_ممکن_نشد = -122,
        به_علت_تناقض_کلید_خارجی_ویرایش_ممکن_نشد = -123,
        به_علت_تناقض_کلید_خارجی_عملیات_حذف_ممکن_نشد = -124,
        درج_یا_ویرایش_برای_فیلد_فقط_خواندنی_مقدور_نیست = -138,
        پارامتر_طول_نامعتبر_به_تابع_SUBSTRING_ارسال_شد = -140,
        مقدار_ورودی_نامعتبر_به_تابع_CONVERT_ارسال_شد = -141,
        امکان_تبدیل_ورودی_تاریخ_به_مقدار_تاریخ_منطقی_معتبر_وجود_ندارد = -146,
        امکان_تبدیل_ورودی_زمان_به_مقدار_زمان_منطقی_معتبر_وجود_ندارد = -147,
        اتصال_SQL_تعریف_نشده_است = -162,
        تلاش_برای_افزودن_فیلد_اجباری_بدون_مقدار_پیش_فرض_به_جدول_دارای_دیتا = -304
    }

    public class Return
    {
        private StatusType _Type;
        private List<ErrorDto> _ErrorList = new();
        private bool _IsSucceed { get; set; } = true;
        private int _Count { get; set; } = 0;
        private int _StatusCode { get; set; } = 0;
        private string _Message { get; set; } = string.Empty;
        private object _Data { get; set; } = null!;

        private ResultDto GetResult()
        {
            #region Get Result
            if (_Data is int)
            {
                var Value = Convert.ToInt32(_Data);

                if (Value == 0)
                {
                    _StatusCode = StatusCode.Bad_Request.EnumToInt();
                    _IsSucceed = Value > 0;
                }
            }
            else if (_Data is IEnumerable) { }
            else
            {
                if (_Data == null)
                {
                    _Count = 0;
                    _StatusCode = _StatusCode != 200 ? StatusCode.Bad_Request.EnumToInt() : _StatusCode;
                    _IsSucceed = false;
                }
                else
                    _Count = 1;
            }

            _Message = _Message == "" ? GetStatusMessage(_IsSucceed) : _Message;

            return new ResultDto()
            {
                Data = _Data,
                Count = _Count,
                Message = _Message,
                IsSucceed = _IsSucceed,
                StatusCode = _StatusCode,
                ErrorList = _ErrorList
            };
            #endregion
        }

        private string GetStatusMessage(bool State)
        {
            #region Get Status Message
            var msg = MessageEnum.عملیات_با_موفقیت_انجام_شد.EnumToString();

            if (!State)
            {
                switch (_Type)
                {
                    case StatusType.ثبت:
                        msg = MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString();
                        break;
                    case StatusType.حذف:
                        msg = MessageEnum.حذف_رکورد_با_مشکل_مواجه_شد.EnumToString();
                        break;
                    case StatusType.دریافت:
                        msg = MessageEnum.رکورد_مورد_نظر_یافت_نشد.EnumToString();
                        break;
                    default:
                        break;
                }
            }
            return msg;
            #endregion
        }

        private static string GetSqlExceptionMessage(int Number)
        {
            #region Get SqlException Message
            var Error = string.Empty;
            switch (Number)
            {
                case -10050:
                    Error = SqlExceptionKeys.شبکه_قطع_است.EnumToString();
                    break;
                case -10051:
                    Error = SqlExceptionKeys.شبکه_در_دسترس_نمی_باشد.EnumToString();
                    break;
                case -10055:
                    Error = SqlExceptionKeys.فضای_بافری_در_دسترس_نیست.EnumToString();
                    break;
                case -10060:
                    Error = SqlExceptionKeys.ارتباط_با_پایگاه_داده_منقضی_شد.EnumToString();
                    break;
                case -10061:
                    Error = SqlExceptionKeys.ارتباط_با_پایگاه_داده_رد_شد.EnumToString();
                    break;
                case -10064:
                    Error = SqlExceptionKeys.سرور_خاموش_است.EnumToString();
                    break;
                case -101:
                    Error = SqlExceptionKeys.تلاش_برای_باز_کردن_اشاره_گری_که_از_قبل_باز_بوده_است.EnumToString();
                    break;
                case -102:
                    Error = SqlExceptionKeys.عملیات_روی_اشاره_گر_بازنشده_انجام_شد.EnumToString();
                    break;
                case -103:
                    Error = SqlExceptionKeys.عملیات_ویرایش_یا_حذف_انجام_شد_اما_اشاره_گر_روی_هیچ_ردیفی_قرار_نگرفت.EnumToString();
                    break;
                case -104:
                    Error = SqlExceptionKeys.اعتبار_سنجی_فیلد_در_درج_اطلاعات_در_سمت_پایگاه_داده_با_خطا_مواجه_شد.EnumToString();
                    break;
                case -105:
                    Error = SqlExceptionKeys.اعتبار_سنجی_فیلد_در_ویرایش_اطلاعات_در_سمت_پایگاه_داده_با_خطا_مواجه_شد.EnumToString();
                    break;
                case -106:
                    Error = SqlExceptionKeys.ردیفی_برای_حذف_یافت_نشد.EnumToString();
                    break;
                case -108:
                    Error = SqlExceptionKeys.فیلد_الزامی_وجود_ندارد_درج_یا_ویرایش_مجاز_نیست.EnumToString();
                    break;
                case -109:
                    Error = SqlExceptionKeys.نمی_توان_ردیف_تعیین_شده_برای_ویرایش_را_پیدا_کرد.EnumToString();
                    break;
                case -112:
                    Error = SqlExceptionKeys.دسترسی_غیر_مجاز.EnumToString();
                    break;
                case -114:
                    Error = SqlExceptionKeys.رکورد_یا_رکوردهای_انتخابی_توسط_کاربر_دیگری_قفل_شده_است.EnumToString();
                    break;
                case -115:
                    Error = SqlExceptionKeys.عملیات_درج_یا_ویرایش_یا_حذف_بر_روی_جدول_فقط_خواندنی_مقدور_نمی_باشد.EnumToString();
                    break;
                case -116:
                    Error = SqlExceptionKeys.عدم_تطابق_اصلی_در_بین_لیست_مقادیر_و_تعداد_ستون_های_جدول.EnumToString();
                    break;
                case -117:
                    Error = SqlExceptionKeys.توابع_تجمعی_در_نماها_پشتیبانی_نمی_شوند.EnumToString();
                    break;
                case -119:
                    Error = SqlExceptionKeys.قید_منحصر_به_فرد_یا_قید_کلید_اصلی_برای_عملیات_درج_ناموفق_بوده_است.EnumToString();
                    break;
                case -120:
                    Error = SqlExceptionKeys.قید_منحصر_به_فرد_یا_قید_کلید_اصلی_برای_عملیات_ویرایش_ناموفق_بوده_است.EnumToString();
                    break;
                case -121:
                    Error = SqlExceptionKeys.به_علت_تناقض_کلید_خارجی_عملیات_درج_ممکن_نشد.EnumToString();
                    break;
                case -122:
                case -123:
                    Error = SqlExceptionKeys.به_علت_تناقض_کلید_خارجی_عملیات_ویرایش_ممکن_نشد.EnumToString();
                    break;
                case -124:
                    Error = SqlExceptionKeys.به_علت_تناقض_کلید_خارجی_عملیات_حذف_ممکن_نشد.EnumToString();
                    break;
                case -138:
                    Error = SqlExceptionKeys.درج_یا_ویرایش_برای_فیلد_فقط_خواندنی_مقدور_نیست.EnumToString();
                    break;
                case -140:
                    Error = SqlExceptionKeys.پارامتر_طول_نامعتبر_به_تابع_SUBSTRING_ارسال_شد.EnumToString();
                    break;
                case -141:
                    Error = SqlExceptionKeys.مقدار_ورودی_نامعتبر_به_تابع_CONVERT_ارسال_شد.EnumToString();
                    break;
                case -146:
                    Error = SqlExceptionKeys.امکان_تبدیل_ورودی_تاریخ_به_مقدار_تاریخ_منطقی_معتبر_وجود_ندارد.EnumToString();
                    break;
                case -147:
                    Error = SqlExceptionKeys.امکان_تبدیل_ورودی_زمان_به_مقدار_زمان_منطقی_معتبر_وجود_ندارد.EnumToString();
                    break;
                case -162:
                    Error = SqlExceptionKeys.اتصال_SQL_تعریف_نشده_است.EnumToString();
                    break;
                case -304:
                    Error = SqlExceptionKeys.تلاش_برای_افزودن_فیلد_اجباری_بدون_مقدار_پیش_فرض_به_جدول_دارای_دیتا.EnumToString();
                    break;
                default:
                    Error = SqlExceptionKeys.خطایی_در_پایگاه_داده_رخ_داده_است.EnumToString();
                    break;
            }

            return Error;
            #endregion
        }

        public ResultDto ReturnException(Exception Exception, string Message = "", StatusCode Code = StatusCode.Bad_Request)
        {
            #region Return Exception
            _Count = 0;
            _IsSucceed = false;
            _StatusCode = (Code == StatusCode.Bad_Request) ? StatusCode.Bad_Request.EnumToInt() : Code.EnumToInt();
            _Message = Message == "" ? MessageEnum.خطایی_رخ_داده_است.EnumToString() : Message;
            var Body = Exception.Message;
            var ErrorCode = Exception.HResult;

            if (Exception.GetType().Name == "SqlException")
            {
                //ErrorCode = ((SqlException)Exception).ErrorCode;

                //Body = GetSqlExceptionMessage(ErrorCode);
            }
            else if (Exception.GetType().Name == "ArithmeticException")
            {
                //ErrorCode = ((ArithmeticException)Exception).Data.Keys;
            }
            else if (Exception.GetType().Name == "Win32Exception")
            {

            }

            var sssssssss = Exception.GetType().Name;

            //TODO: Getting List of all exception and check them and get them number


            if (!string.IsNullOrEmpty(Body))
            {
                _ErrorList = new()
                {
                    new ErrorDto()
                    {
                        ErrorMessage = Body,
                        ErrorCode = Exception.HResult
                    }
                };
            }

            return GetResult();
            #endregion
        }

        public ResultDto ReturnData(object Data, StatusType Type, StatusCode Code = StatusCode.Ok, string Message = "", int Count = 0)
        {
            #region Return Result
            _Count = Count;
            _IsSucceed = true;
            _StatusCode = Code.EnumToInt();
            _Message = Message;
            _Data = Data;
            _Type = Type;

            return GetResult();
            #endregion
        }

        public ResultDto ReturnValidation(List<FluentValidation.Results.ValidationFailure> ErrorList)
        {
            #region Return Exception
            _Count = 0;
            _IsSucceed = false;
            _StatusCode = StatusCode.Bad_Request.EnumToInt();
            _Message = MessageEnum.خطایی_رخ_داده_است.EnumToString();

            foreach (var Error in ErrorList)
            {
                _ErrorList.Add(
                    new ErrorDto()
                    {
                        ErrorMessage = Error.ErrorMessage
                    });
            }

            return GetResult();
            #endregion
        }


    }
}
